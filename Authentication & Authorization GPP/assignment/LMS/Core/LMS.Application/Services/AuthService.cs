using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

                return new ResponseModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpiredOn = token.ValidTo,
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

            // if (role == "Library Manager" || role == "Librarian")
            // {
            //     AppUser user = new AppUser()
            //     {
            //         Email = model.Email,
            //         SecurityStamp = Guid.NewGuid().ToString(),
            //         UserName = model.Username,
            //         FirstName = model.FirstName,
            //         LastName = model.LastName,
            //         LibraryCard = new LibraryCard
            //         {
            //             CardNumber = Guid.NewGuid().ToString(),
            //             ExpiryDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(3)
            //         }
            //     };

            //     var result = await _userManager.CreateAsync(user, model.Password);

            //     if (!result.Succeeded)
            //     {
            //         return new ResponseModel
            //         {
            //             Status = "Error",
            //             Message = "User creation failed! Please check user details and try again."
            //         };
            //     }

            //     if (await _roleManager.RoleExistsAsync(role))
            //     {
            //         await _userManager.AddToRoleAsync(user, role);
            //     }

            //     return new ResponseModel
            //     {
            //         Status = "Success",
            //         Message = "User created successfully!"
            //     };
            // } else
            // {
            //     AppUser user = new AppUser()
            //     {
            //         Email = model.Email,
            //         SecurityStamp = Guid.NewGuid().ToString(),
            //         UserName = model.Username,
            //         FirstName = model.FirstName,
            //         LastName = model.LastName,
            //     };

            //     var result = await _userManager.CreateAsync(user, model.Password);

            //     if (!result.Succeeded)
            //     {
            //         return new ResponseModel
            //         {
            //             Status = "Error",
            //             Message = "User creation failed! Please check user details and try again."
            //         };
            //     }

            //     if (await _roleManager.RoleExistsAsync(role))
            //     {
            //         await _userManager.AddToRoleAsync(user, role);
            //     }

            //     return new ResponseModel
            //     {
            //         Status = "Success",
            //         Message = "User created successfully!"
            //     };
            // }
        }
    }
}