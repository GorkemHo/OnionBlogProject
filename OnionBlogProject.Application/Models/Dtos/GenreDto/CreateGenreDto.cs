using OnionBlogProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Application.Models.Dtos.GenreDto
{
    public class CreateGenreDto
    {
        [Required(ErrorMessage = "Tür Adı Boş Bırakılamaz.")]
        //[Display(Name = "Tür Adı")]
        [MinLength(3, ErrorMessage = "Tür adı en az 3 harften oluşmalıdır.")]
        public string Name { get; set; }

        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
