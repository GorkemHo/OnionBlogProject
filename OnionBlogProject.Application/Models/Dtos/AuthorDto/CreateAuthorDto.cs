using Microsoft.AspNetCore.Http;
using OnionBlogProject.Application.Extensions;
using OnionBlogProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Application.Models.Dtos.AuthorDto
{
    public class CreateAuthorDto
    {
        [Required(ErrorMessage = "İsim Boş Bırakılamaz.")]
        [Display(Name = "İsim")]
        [MinLength(3,ErrorMessage ="İsim en az 3 harften oluşmalıdır.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyisim Boş Bırakılamaz.")]
        [Display(Name = "Soyisim")]
        [MinLength(2, ErrorMessage = "Soyisim en az 2 harften oluşmalıdır.")]
        public string LastName { get; set; }

        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
