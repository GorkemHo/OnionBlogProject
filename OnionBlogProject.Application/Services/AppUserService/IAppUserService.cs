using Microsoft.AspNetCore.Identity;
using OnionBlogProject.Application.Models.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Application.Services.AppUserService
{
    public interface IAppUserService
    {
        Task<IdentityResult> Register(RegisterDto model);
        Task<SignInResult> Login(LoginDto model);

        Task Logout();
        Task UpdateUser(UpdateProfileDto model);
        Task<UpdateProfileDto> GetByUserName(string userName);

        Task<bool> UserInRole(string userName, string role);
    }
}
