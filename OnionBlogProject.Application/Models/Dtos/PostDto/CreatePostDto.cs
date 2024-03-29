using Microsoft.AspNetCore.Http;
using OnionBlogProject.Application.Extensions;
using OnionBlogProject.Application.Models.Vms;
using OnionBlogProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Application.Models.Dtos.PostDto
{
    public class CreatePostDto
    {

        [Required(ErrorMessage = "Gönderi Başlığı Boş Bırakılamaz.")]
        [MinLength(3, ErrorMessage = "Gönderi Başlığı en az 3 harften oluşmalıdır.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Gönderi Başlığı Boş Bırakılamaz.")]
        [MinLength(3, ErrorMessage = "Gönderi Başlığı en az 3 harften oluşmalıdır.")]
        public string Content { get; set; }
        

        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;

        [Required(ErrorMessage = "Yazar Seçilmek zorundadır.")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Tür Seçilmek zorundadır.")]
        public int GenreId { get; set; }

        public List<GenreVm>? Genres { get; set; }
        public List<AuthorVm>? Authors { get; set; }
    }
}
