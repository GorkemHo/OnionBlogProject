using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnionBlogProject.Application.Ioc;
using OnionBlogProject.Domain.Entities;
using OnionBlogProject.Infrastructure.Context;

namespace OnionBlogProject.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString)); Ýçeriye connection string yazmasak da büyük bir sýkýntý çýkartmaz zaten class içerisinde tanýmlamamýzý yaptýk.
            builder.Services.AddDbContext<AppDbContext>();

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = false;
                opt.SignIn.RequireConfirmedPhoneNumber = false;
                opt.SignIn.RequireConfirmedAccount = false;

                opt.User.RequireUniqueEmail = false;

                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //builder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("admin", policy => policy.RequireRole("admin"));
            //    options.AddPolicy("member", policy => policy.RequireRole("member"));
            //});

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new DependencyResolver());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}