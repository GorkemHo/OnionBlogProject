using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnionBlogProject.Domain.Entities;
using OnionBlogProject.Domain.Repositories;
using OnionBlogProject.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Infrastructure.Repositories
{
    public class AppUserRepo : BaseRepo<AppUser>, IAppUserRepo
    {
        UserManager<AppUser> _userManager; //UserManager Identity User için doğal bir repository olarak kabul edilebilir. Crud işlemlerini kendi bünyesinde yapar.
        public AppUserRepo(AppDbContext context, UserManager<AppUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public override Task Create(AppUser entity) // Burda aslında adapter design pattern kullanmış olduk. Manager zaten kaydetme işlemi yapıyor BaseRepoya bu görevi vermeye gerek yok. Sadece savechanges yapabilmek için base.Create metoduna yönlendirme yaptık.
        {
            _userManager.CreateAsync(entity);

            return base.Create(entity);
        }

        //public override async Task Create(AppUser entity)
        //{
        //    await _userManager.CreateAsync(entity);

        //    await base.Create(entity);
        //}
    }
}
