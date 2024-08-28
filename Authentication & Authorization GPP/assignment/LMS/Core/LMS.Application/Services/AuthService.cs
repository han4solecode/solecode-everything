using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LMS.Application.Contracts;
using LMS.Application.DTOs.Login;
using LMS.Application.DTOs.Register;
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LMS.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<AppUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<ResponseModel> CreateRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            return new ResponseModel
            {
                Status = "Success",
                Message = "Role created successfully!"
            };
        }

        public async Task<ResponseModel> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]!));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                // check if refresh token is exist and valid
                if (user.RefreshToken != null && user.RefreshTokenExpiryTime > DateTime.UtcNow)
                {
                    return new ResponseModel
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        ExpiredOn = token.ValidTo,
                        RefreshToken = user.RefreshToken,
                        RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                        Status = "Success",
                        Message = "Login successful!"
                    };
                }

                // if refresh token is null or invalid, generate refresh token and update user
                var refreshToken = GenerateRefreshToken();
                // var refreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                var refreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(3);

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = refreshTokenExpiryTime;

                await _userManager.UpdateAsync(user);

                return new ResponseModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpiredOn = token.ValidTo,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = refreshTokenExpiryTime,
                    Status = "Success",
                    Message = "Login successful!"
                };
            }

            return new ResponseModel
            {
                Status = "Error",
                Message = "Username or password is not valid!"
            };
        }

        public async Task<ResponseModel> LogoutAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "User not found"
                };
            }

            if (user.RefreshToken == null)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "User already logged out"
                };
            }

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _userManager.UpdateAsync(user);

            return new ResponseModel
            {
                Status = "Success",
                Message = "User logged out successfully"
            };
        }

        public async Task<ResponseModel> RefreshAccessToken(string refreshToken)
        {
            var user = await _userManager.Users.Where(u => u.RefreshToken == refreshToken).SingleOrDefaultAsync();

            if (user == null)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = $"User with {refreshToken} refresh token does not exist."
                };
            }

            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "Refresh token is not valid. Please log in.",
                };
            } else
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]!));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    expires: DateTime.Now.AddMinutes(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return new ResponseModel
                {
                    Status = "Success",
                    Message = "Access token refreshed successfully!",
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpiredOn = token.ValidTo,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
                };
            }
        }

        public async Task<ResponseModel> RegisterLibrarian(RegisterModel model)
        {
            var res = await CreateUser(model, "Librarian");

            return res;
        }

        public async Task<ResponseModel> RegisterLibraryManager(RegisterModel model)
        {
            var res = await CreateUser(model, "Library Manager");

            return res;
        }

        public async Task<ResponseModel> RegisterLibraryUser(RegisterModel model)
        {
            var res = await CreateUser(model, "Library User");

            return res;
        }

        public async Task<ResponseModel> SignUpAsync(RegisterModel model)
        {
            var userExist = await _userManager.FindByNameAsync(model.Username);

            if (userExist != null)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "User already exist!"
                };
            }

            AppUser user = new AppUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "User creation failed! Please check user details and try again."
                };
            }

            // await _userManager.AddToRoleAsync(user, "User");
            if (await _roleManager.RoleExistsAsync("User"))
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return new ResponseModel
            {
                Status = "Success",
                Message = "User created successfully!"
            };
        }

        private static string GenerateRefreshToken()
        {
            var randNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randNumber);
                return Convert.ToBase64String(randNumber);
            }
        }

        private async Task<ResponseModel> CreateUser(RegisterModel model, string role)
        {
            var userExist = await _userManager.FindByNameAsync(model.Username);

            if (userExist != null)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "User already exist!"
                };
            }

            AppUser user = new AppUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                LibraryCard = new LibraryCard
                {
                    CardNumber = Guid.NewGuid().ToString(),
                    ExpiryDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(3)
                }
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new ResponseModel
                {
                    Status = "Error",
                    Message = "User creation failed! Please check user details and try again."
                };
            }

            if (await _roleManager.RoleExistsAsync(role))
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            return new ResponseModel
            {
                Status = "Success",
                Message = "User created successfully!"
            };
        }
    }
}