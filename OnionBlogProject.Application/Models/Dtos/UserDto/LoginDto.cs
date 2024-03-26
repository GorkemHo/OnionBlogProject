using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Application.Models.Dtos.UserDto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Kullanıcı Adı Boş Bırakılamaz.")]
        [Display(Name = "Kullanıcı Adı")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Şifre Boş Bırakılamaz.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
