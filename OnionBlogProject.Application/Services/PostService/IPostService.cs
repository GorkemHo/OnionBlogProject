using OnionBlogProject.Application.Models.Dtos.PostDto;
using OnionBlogProject.Application.Models.Vms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Application.Services.PostService
{
    public interface IPostService
    {
        Task Create(CreatePostDto model);
        Task Update(UpdatePostDto model);
        Task Delete(int id);
        Task<List<PostVm>> GetPosts();
        Task<PostDetailsVm> GetPostDetailsVm(int id);
        Task<UpdatePostDto> GetById(int id);
        Task<List<GetPostsVm>> GetPostsForMembers(); // Kişilerin postlarını döndür.
        Task<CreatePostDto> CreatePost(); // Post Create işleminde ilk adımda View'a giderken, Genre ve Author listesini doldurmak için bu metodu kullanacağız.
    }
}
