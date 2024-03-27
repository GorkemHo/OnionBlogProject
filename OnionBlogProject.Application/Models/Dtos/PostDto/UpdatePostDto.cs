using Microsoft.AspNetCore.Http;
using OnionBlogProject.Application.Extensions;
using OnionBlogProject.Application.Models.Vms;
using OnionBlogProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Application.Models.Dtos.PostDto
{
    public class UpdatePostDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Gönderi Başlığı Boş Bırakılamaz.")]
        [MinLength(3, ErrorMessage = "Gönderi Başlığı en az 3 harften oluşmalıdır.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Gönderi Başlığı Boş Bırakılamaz.")]
        [MinLength(3, ErrorMessage = "Gönderi Başlığı en az 3 harften oluşmalıdır.")]
        public string Content { get; set; }
        public string ImagePath { get; set; }

        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
               
        public int AuthorId { get; set; }        
        public int GenreId { get; set; }
        public List<GenreVm>? Genres { get; set; }
        public List<AuthorVm>? Authors { get; set; }
    }
}
