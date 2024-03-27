using Autofac;
using AutoMapper;
using OnionBlogProject.Application.AutoMapper;
using OnionBlogProject.Application.Services.AppUserService;
using OnionBlogProject.Application.Services.AuthorService;
using OnionBlogProject.Application.Services.GenreService;
using OnionBlogProject.Application.Services.PostService;
using OnionBlogProject.Domain.Repositories;
using OnionBlogProject.Infrastructure.Repositories;

namespace OnionBlogProject.Application.Ioc
{
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppUserRepo>().As<IAppUserRepo>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorRepo>().As<IAuthorRepo>().InstancePerLifetimeScope();
            builder.RegisterType<GenreRepo>().As<IGenreRepo>().InstancePerLifetimeScope();
            builder.RegisterType<PostRepo>().As<IPostRepo>().InstancePerLifetimeScope();

            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerLifetimeScope();
            builder.RegisterType<GenreService>().As<IGenreService>().InstancePerLifetimeScope();
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();

            // Mapper'ı da Hep IMapper olarak çağırdık bu sebeple Burada Mapper class ı ile IMapper ı resolve ediyoruz.
            builder.Register(context => new MapperConfiguration(config =>
            {
                // Register Mapper Profile
                config.AddProfile<Mapping>();
            })).AsSelf().SingleInstance();

            builder.Register(c =>{
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);

            })
                .As<IMapper>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
