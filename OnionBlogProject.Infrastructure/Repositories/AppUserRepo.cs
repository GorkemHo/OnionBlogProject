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
        UserManager<AppUser> _userManager;
        public AppUserRepo(AppDbContext context, DbSet<AppUser> table, UserManager<AppUser> userManager) : base(context, table)
        {
            _userManager = userManager;
        }

        public override Task Create(AppUser entity)
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
