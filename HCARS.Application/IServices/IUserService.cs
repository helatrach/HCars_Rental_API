using HCARS.Domain.EntitiesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Services.IServices
{
    public interface IUserService
    {
        Task<UserResponseModel> RegisterUserAsync(RegisterModel model);
        Task<UserResponseModel> LoginUserAsync(LoginModel model);
    }
}
