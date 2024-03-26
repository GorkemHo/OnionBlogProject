using OnionBlogProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Application.Models.Dtos.UserDto
{
    public class RegisterDto
    {
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
        public DateTime CreatedDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
