using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnionBlogProject.Application.Models.Dtos.PostDto;
using OnionBlogProject.Application.Models.Vms;
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

namespace OnionBlogProject.Application.Services.PostService
{
    internal class PostService : IPostService
    {
        private readonly IPostRepo _postRepo;
        private readonly IGenreRepo _genreRepo;
        private readonly IAuthorRepo _authorRepo;
        private readonly IMapper _mapper;

        public PostService(IMapper mapper, IAuthorRepo authorRepo, IGenreRepo genreRepo, IPostRepo postRepo)
        {
            _mapper = mapper;
            _authorRepo = authorRepo;
            _genreRepo = genreRepo;
            _postRepo = postRepo;
        }

        public async Task Create(CreatePostDto model)
        {
            var post = _mapper.Map<Post>(model);

            if (post.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                post.ImagePath = $"/images/{guid}.jpg";

                await _postRepo.Create(post);
            }
            else
            {
                post.ImagePath = $"/images/default.jpg";
                await _postRepo.Create(post);
            }
        }

        public async Task<CreatePostDto> CreatePost()
        {
            //CreatePostDto model = new CreatePostDto()
            //{
            //    Genres = await _genreRepo.GetFilteredList(
            //        select: x => new GenreVm
            //        {
            //            Id = x.Id,
            //            Name = x.Name,
            //        }, where: x => x.Status != Status.Passive,
            //        orderby: x => x.OrderBy(x => x.Name)),

            //    Authors = await _authorRepo.GetFilteredList(
            //        select: x => new AuthorVm
            //        {
            //            Id = x.Id,
            //            FirstName = x.FirstName,
            //            LastName = x.LastName,
            //        }, where: x => x.Status != Status.Passive,
            //        orderby: x => x.OrderBy(x => x.FirstName)),
            //};

            //return model;

            CreatePostDto model = new CreatePostDto()
            {
                Genres = await _genreRepo.GetFilteredList(
           select: x => new GenreVm
           {
               Id = x.Id,
               Name = x.Name,
           },
           where: x => x.Status != Status.Passive,
           orderby: x => x.OrderBy(x => x.Name)),
                Authors = await _authorRepo.GetFilteredList(
           select: x => new AuthorVm
           {
               Id = x.Id,
               FirstName = x.FirstName,
               LastName = x.LastName,
           },
           where: x => x.Status != Status.Passive,
           orderby: x => x.OrderBy(x => x.FirstName))
            };

            return model;
        }

        public async Task Delete(int id)
        {
            var post = await _postRepo.GetDeault(x => x.Id.Equals(id));

            if (post is not null)
            {
                post.DeleteDate = DateTime.Now;
                post.Status = Status.Passive;
                await _postRepo.Update(post);
            }
        }

        public async Task<UpdatePostDto> GetById(int id)
        {
            var post = await _postRepo.GetFilteredList(select: x => new PostVm
            {
                Title = x.Title,
                Content = x.Content,
                ImagePath = x.ImagePath,
                GenreId = x.GenreId,
                AuthorId = x.AuthorId
            }, where: x => x.Id == id);

            var model = _mapper.Map<UpdatePostDto>(post);

            model.Authors = await _authorRepo.GetFilteredList(
                select: x => new AuthorVm
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                }, where: x => x.Status != Status.Passive,
                orderby: x => x.OrderBy(x => x.FirstName));

            model.Genres = await _genreRepo.GetFilteredList(
                select: x => new GenreVm
                {
                    Id = x.Id,
                    Name = x.Name
                }, where: x => x.Status != Status.Passive,
                orderby: x => x.OrderBy(x => x.Name));

            return model;

        }

        public async Task<PostDetailsVm> GetPostDetailsVm(int id)
        {
            var post = await _postRepo.GetFilteredFirstOrDefault(select: x => new PostDetailsVm
            {
                Title = x.Title,
                Content = x.Content,
                ImagePath = x.ImagePath,
                CreateDate = x.CreateDate,
                AuthorFirstName = x.Author.FirstName,
                AuthorLastName = x.Author.LastName,
                AuthorImagePath = x.ImagePath,
            },
            where: x => x.Id == id,
            orderby: x => x.OrderBy(x => x.Title),
            include: x => x.Include(x => x.Author));

            return post;
        }

        public async Task<List<PostVm>> GetPosts()
        {
            var posts = await _postRepo.GetFilteredList(select: x => new PostVm
            {
                Id = x.Id,
                Title = x.Title,
                GenreName = x.Genre.Name,
                AuthorFirstName = x.Author.FirstName,
                AuthorLastName = x.Author.LastName,


            }, where: x => x.Status != Status.Passive,
            orderby: x => x.OrderBy(x => x.Title),
            include: x => x.Include(x => x.Author).Include(x => x.Genre));

            return posts;
        }

        public async Task<List<GetPostsVm>> GetPostsForMembers()
        {
            var posts = await _postRepo.GetFilteredList(select: x => new GetPostsVm
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                ImagePath = x.ImagePath,
                CreateDate = x.CreateDate,
                AuthorFirstName = x.Author.FirstName,
                AuthorLastName = x.Author.LastName,
                UserImagePath = x.Author.ImagePath,

            }, where: x => x.Status != Status.Passive,
           orderby: x => x.OrderByDescending(x => x.CreateDate),
           include: x => x.Include(x => x.Author));

            return posts;
        }

        public async Task Update(UpdatePostDto model)
        {
            var post = _mapper.Map<Post>(model);

            if (post.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                post.ImagePath = $"/images/{guid}.jpg";

                await _postRepo.Update(post);
            }
            else
            {
                post.ImagePath = model.ImagePath;
                await _postRepo.Update(post);
            }
        }
    }
}
