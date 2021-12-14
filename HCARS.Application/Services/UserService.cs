using HCARS.Domain.EntitiesModels;
using HCARS.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HCARS.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private IConfiguration _config;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<UserResponseModel> LoginUserAsync(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return new UserResponseModel
                {
                    Message = "There's no account with this Email address",
                    IsSuccess = false,
                };
            }
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return new UserResponseModel { Message = "Invalid password", IsSuccess = false };
            }

            var Claims = new[]
            {
                new Claim("Email" , model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _config["AuthSettings:Issuer"],
                audience: _config["AuthSettings:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials : new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            string TokenAsString = new JwtSecurityTokenHandler().WriteToken(token);


            return new UserResponseModel()
            {
                Message = TokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };
        }

        public async Task<UserResponseModel> RegisterUserAsync(RegisterModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register is null");

            if (model.Password != model.ConfirmPassword)
                return new UserResponseModel
                {
                    Message = "Confirm Password Doesn't match the password",
                    IsSuccess = false,
                };

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);
            if (result.Succeeded)
            {
                return new UserResponseModel()
                {
                    Message = "User Created successfully !",
                    IsSuccess = true,
                };

            }

            return new UserResponseModel()
            { Message = "User is not created",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }
}
