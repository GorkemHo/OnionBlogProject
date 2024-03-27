using AutoMapper;
using OnionBlogProject.Application.Models.Dtos.AuthorDto;
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

namespace OnionBlogProject.Application.Services.AuthorService
{
    internal class AuthorService : IAuthorService
    {
        private IAuthorRepo repo;
        private IMapper mapper;

        public AuthorService(IMapper mapper, IAuthorRepo repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        public async Task Create(CreateAuthorDto model)
        {
            var author = mapper.Map<Author>(model);

            if(author.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                author.ImagePath = $"/images/{guid}.jpg";

                await repo.Create(author);
            }
            else
            {
                author.ImagePath = $"/images/default.jpg";
                await repo.Create(author);
            }
        }

        public async Task Delete(int id)
        {
           var author = await repo.GetDeault(x => x.Id.Equals(id));

            if(author is not null)
            {
                author.DeleteDate = DateTime.Now;
                author.Status = Status.Passive;
                await repo.Update(author);
            }
        }

        public async Task<List<AuthorVm>> GetAuthors()
        {
            var authors = await repo.GetFilteredList(select: x => mapper.Map<AuthorVm>(x),where: x => !x.Status.Equals(Status.Passive),orderby: x=> x.OrderBy(x => x.FirstName));

            return authors;
        }

        public async Task<UpdateAuthorDto> GetById(int id)
        {
            var author = await repo.GetFilteredFirstOrDefault(select: x => mapper.Map<UpdateAuthorDto>(x),where: x => x.Id.Equals(id) && !x.Status.Equals(Status.Passive));

            return author;
        }

        public async Task<AuthorDetailVm> GetDetails(int id)
        {
            var author = await repo.GetFilteredFirstOrDefault(select: x => mapper.Map<AuthorDetailVm>(x), where: x => x.Id.Equals(id) && !x.Status.Equals(Status.Passive));

            return author;
        }

        public async Task<bool> IsAuthorExist(string firstName, string lastName)
        {
            var result = await repo.Any(x => x.FirstName.Equals(firstName) && x.LastName.Equals(lastName));

            return result;
        }

        public async Task Update(UpdateAuthorDto model)
        {
            var author = mapper.Map<Author>(model);

            if(author.UploadPath == null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.png");
                author.ImagePath = $"/images/{guid}.png";

                await repo.Update(author);
            }
            else
            {
                author.ImagePath = model.ImagePath;
                await repo.Update(author);
            }
        }
    }
}
