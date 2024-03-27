using Microsoft.AspNetCore.Http;
using OnionBlogProject.Domain.Entities;
using OnionBlogProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Application.Models.Vms
{
    public class PostDetailsVm
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }

        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorImagePath { get; set; }

        public DateTime CreateDate { get; set; }



    }
}
