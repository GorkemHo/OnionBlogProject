using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnionBlogProject.Application.Models.Dtos.UserDto;
using OnionBlogProject.Domain.Entities;
using OnionBlogProject.Domain.Enums;
using OnionBlogProject.Domain.Repositories;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Application.Services.AppUserService
{
    public class AppUserService : IAppUserService
    {
        private IAppUserRepo repo;
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IMapper mapper;

        public AppUserService(IAppUserRepo repo, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            this.repo = repo;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public async Task<UpdateProfileDto> GetByUserName(string userName)
        {
            var user = await repo.GetFilteredFirstOrDefault(select: x => mapper.Map<UpdateProfileDto>(x), where: x => x.UserName.Equals(userName) && !x.Status.Equals(Status.Passive));

            return user;
        }

        public async Task<SignInResult> Login(LoginDto model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            return result;
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDto model)
        {
            var user = mapper.Map<AppUser>(model);

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);

            }
            return result;
        }

        public async Task UpdateUser(UpdateProfileDto model)
        {
            var user = await repo.GetDeault(x => x.Equals(model.Id));

            await ImageUpload(model, user);

            await UserNameUpdate(model, user);

            await EmailUpdate(model, user);
        }

        private async Task EmailUpdate(UpdateProfileDto model, AppUser user)
        {
            if (model.Email != null)
            {
                var isUserEmailExist = await userManager.FindByEmailAsync(model.Email.ToUpper());

                if (isUserEmailExist != null)
                {
                    await userManager.SetEmailAsync(user, model.Email);
                }

            }
        }

        private async Task UserNameUpdate(UpdateProfileDto model, AppUser user)
        {
            if (model.Password != null)
            {
                var isUserNameExist = await userManager.FindByNameAsync(model.UserName.ToUpper()); // username.toupper da olabilir.

                if (isUserNameExist != null)
                {
                    await userManager.SetUserNameAsync(user, model.UserName);
                    await signInManager.SignInAsync(user, false);
                }
            }
        }

        private async Task ImageUpload(UpdateProfileDto model, AppUser user)
        {
            if (model.UploadPath is not null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                user.ImagePath = $"/images/{guid}.jpg";

                await repo.Update(user);
            }
        }
    }
}
