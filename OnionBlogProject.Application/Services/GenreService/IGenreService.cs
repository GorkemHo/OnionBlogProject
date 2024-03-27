using OnionBlogProject.Application.Models.Dtos.GenreDto;
using OnionBlogProject.Application.Models.Vms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Application.Services.GenreService
{
    public interface IGenreService
    {
        Task Create(CreateGenreDto model);
        Task Update(UpdateGenreDto model);
        Task Delete(int id);
        Task<List<GenreVm>> GetGenres();

        Task<UpdateGenreDto> GetById(int id);

        Task<bool> IsGenreExist(string name);
    }
}
