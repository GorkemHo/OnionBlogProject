using Microsoft.AspNetCore.Http;
using OnionBlogProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Domain.Entities
{
    public class Author : IBaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string ImagePath { get; set; }
        [NotMapped] // Db'de Görünmesin.
        public IFormFile UploadPath { get; set; } // Using AspNetCore.Http.Features

        //FROM BASE ENTITY
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        //Nav Properties
        public List<Post> Posts { get; set; }
    }
}
