using AutoMapper;
using OnionBlogProject.Application.Models.Dtos.GenreDto;
using OnionBlogProject.Application.Models.Vms;
using OnionBlogProject.Domain.Entities;
using OnionBlogProject.Domain.Enums;
using OnionBlogProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Application.Services.GenreService
{
    internal class GenreService : IGenreService
    {
        private readonly IGenreRepo _genreRepo;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepo genreRepo, IMapper mapper)
        {
            _genreRepo = genreRepo;
            _mapper = mapper;
        }

        public async Task Create(CreateGenreDto model)
        {
            var genre = _mapper.Map<Genre>(model);
            await _genreRepo.Create(genre);
        }

        public async Task Delete(int id)
        {
            Genre genre = await _genreRepo.GetDeault(x => x.Id == id);
            genre.DeleteDate = DateTime.Now;
            genre.Status = Status.Passive; // Soft delete mantığında veriler silinmez. Pasif olarak işaretlenir.
        }

        public async Task<UpdateGenreDto> GetById(int id)
        {
            var genre = await _genreRepo.GetFilteredFirstOrDefault(select: x => new GenreVm
            {
                Id = x.Id,
                Name = x.Name,

            },
            where: x => x.Id == id);

            var model = _mapper.Map<UpdateGenreDto>(genre);

            return model;
        }

        public async Task<List<GenreVm>> GetGenres()
        {
            var genres = await _genreRepo.GetFilteredList(select: x => new GenreVm
            {
                Id = x.Id,
                Name = x.Name,

            },
           where: x => x.Status != Status.Passive,
           orderby: x => x.OrderBy(x => x.Name));

            return genres;
        }

        public Task<bool> IsGenreExist(string name)
        {
            var result = _genreRepo.Any(x => x.Name == name);

            return result;
        }

        public async Task Update(UpdateGenreDto model)
        {
            var updatedGenre = _mapper.Map<Genre>(model);
            await _genreRepo.Update(updatedGenre);

            

        }
    }
}
