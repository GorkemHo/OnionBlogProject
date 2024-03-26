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

namespace OnionBlogProject.Application.Models.Dtos.UserDto
{
    public class UpdateProfileDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı Adı Boş Bırakılamaz.")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre Boş Bırakılamaz.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre Tekrarı Boş Bırakılamaz.")]
        [Display(Name = "Şifre Tekrarı")]
        [DataType(DataType.Password, ErrorMessage = "Şifreniz gerekli kriterleri sağlamıyor.")]
        [Compare(nameof(Password))]
        public string ConfirmedPassword { get; set; }

        [Required(ErrorMessage = "E-Posta Boş Bırakılamaz.")]
        [Display(Name = "E-Posta")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Geçerli Bir E-Posta adresi giriniz.")]
        public string Email { get; set; }

        [Display(Name = "Resim Dosyası")]
        public string? ImagePath { get; set; }

        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }
        public DateTime UpdatedDate => DateTime.Now;
        public Status Status => Status.Modified;
    }
}
