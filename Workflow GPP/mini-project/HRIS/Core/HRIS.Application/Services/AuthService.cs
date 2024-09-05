using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HRIS.Application.Contracts;
using HRIS.Application.DTOs;
using HRIS.Application.DTOs.Login;
using HRIS.Application.DTOs.RefreshToken;
using HRIS.Application.DTOs.Register;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HRIS.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<AppRole> _roleManager;

        public AuthService(UserManager<Employee> userManager, IConfiguration configuration, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginRequest.Password))
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
                    return new LoginResponseDto
                    {
                        AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                        AccessTokenExpiryTime = token.ValidTo,
                        RefreshToken = user.RefreshToken,
                        RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                        Status = "Success",
                        Message = "Login successful!"
                    };
                }

                // if refresh token is null or invalid, generate refresh token and update user
                var refreshToken = GenerateRefreshToken();
                var refreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(3);

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = refreshTokenExpiryTime;
                await _userManager.UpdateAsync(user);

                return new LoginResponseDto
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    AccessTokenExpiryTime = token.ValidTo,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = refreshTokenExpiryTime,
                    Status = "Success",
                    Message = "Login successful!"
                };
            }

            return new LoginResponseDto
            {
                Status = "Error",
                Message = "Username or password is not valid!"
            };
        }

        public async Task<RefreshTokenResponseDto> RefreshAccessTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.Where(u => u.RefreshToken == refreshToken).SingleOrDefaultAsync();

            if (user == null)
            {
                return new RefreshTokenResponseDto
                {
                    Status = "Error",
                    Message = $"User with {refreshToken} refresh token does not exist."
                };
            }

            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return new RefreshTokenResponseDto
                {
                    Status = "Error",
                    Message = "Refresh token is not valid. Please log in.",
                };
            }
            else
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
                    expires: DateTime.Now.AddDays(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return new RefreshTokenResponseDto
                {
                    Status = "Success",
                    Message = "Access token refreshed successfully!",
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    AccessTokenExpiryTime = token.ValidTo,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
                };
            }
        }

        public async Task<RegisterResponseDto> RegisterEmployeeAsync(RegisterRequestDto registerRequest, string roleName)
        {
            var userExist = await _userManager.FindByNameAsync(registerRequest.Username);

            if (userExist != null)
            {
                return new RegisterResponseDto
                {
                    Status = "Error",
                    Message = "Employee already exist!"
                };
            }

            // var empDependents = new List<EmpDependent>();

            // if (registerRequest.EmpDependent != null)
            // {
            //     foreach (var item in registerRequest.EmpDependent)
            //     {
            //         empDependents.Add(item!);
            //     }
            // }

            var employee = new Employee()
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                Address = registerRequest.Address,
                SecurityStamp = Guid.NewGuid().ToString(),
                Fname = registerRequest.Fname,
                Lname = registerRequest.Lname,
                Ssn = registerRequest.Ssn,
                Dob = registerRequest.Dob,
                Sex = registerRequest.Sex,
                Employmenttype = registerRequest.Employmenttype,
                Salary = registerRequest.Salary,
                Status = registerRequest.Status,
                Level = registerRequest.Level,
                Supervisorempno = registerRequest.Supervisorempno,
                Empdependents = registerRequest.EmpDependent!,
                Deptno = registerRequest.Deptno
            };

            var res = await _userManager.CreateAsync(employee, registerRequest.Password);

            if (!res.Succeeded)
            {
                return new RegisterResponseDto
                {
                    Status = "Error",
                    Message = "Employee registration failed! Please check user details and try again."
                };
            }

            if (await _roleManager.RoleExistsAsync(roleName))
            {
                await _userManager.AddToRoleAsync(employee, roleName);
            }

            return new RegisterResponseDto
            {
                Status = "Success",
                Message = "Employee created successfully!",
                EmployeeData = employee
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
    }
}