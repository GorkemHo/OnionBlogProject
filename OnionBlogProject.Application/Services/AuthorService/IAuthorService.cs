using OnionBlogProject.Application.Models.Dtos.AuthorDto;
using OnionBlogProject.Application.Models.Vms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Application.Services.AuthorService
{
    public interface IAuthorService
    {
        Task Create(CreateAuthorDto model);
        Task Update(UpdateAuthorDto model);
        Task Delete(int id);
        Task<List<AuthorVm>> GetAuthors();

        Task<AuthorDetailVm> GetDetails (int id);

        Task<UpdateAuthorDto> GetById(int id);

        Task<bool> IsAuthorExist(string firstName, string lastName);    
    }
}
