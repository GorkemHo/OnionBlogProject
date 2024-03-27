using AutoMapper;
using OnionBlogProject.Application.Models.Dtos.AuthorDto;
using OnionBlogProject.Application.Models.Dtos.GenreDto;
using OnionBlogProject.Application.Models.Dtos.PostDto;
using OnionBlogProject.Application.Models.Dtos.UserDto;
using OnionBlogProject.Application.Models.Vms;
using OnionBlogProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AppUser, RegisterDto>().ReverseMap();
            CreateMap<AppUser, LoginDto>().ReverseMap();
            CreateMap<AppUser, UpdateProfileDto>().ReverseMap();

            CreateMap<Author, CreateAuthorDto>().ReverseMap();
            CreateMap<Author, UpdateAuthorDto>().ReverseMap();
            CreateMap<Author, AuthorVm>().ReverseMap();
            CreateMap<Author, AuthorDetailVm>().ReverseMap();
            CreateMap<UpdateAuthorDto, AuthorVm>().ReverseMap();

            CreateMap<Genre, CreateGenreDto>().ReverseMap();
            CreateMap<Genre, UpdateGenreDto>().ReverseMap();
            CreateMap<Genre, GenreVm>().ReverseMap();
            CreateMap<UpdateGenreDto, GenreVm>().ReverseMap();

            CreateMap<Post, CreatePostDto>().ReverseMap();
            CreateMap<Post, UpdatePostDto>().ReverseMap();
            CreateMap<Post, PostVm>().ReverseMap();
            CreateMap<UpdateGenreDto, PostVm>().ReverseMap();

        }
    }
}
