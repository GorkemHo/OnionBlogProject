using Microsoft.AspNetCore.Http;
using OnionBlogProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Application.Models.Vms
{
    public class GetPostsVm
    {
        //public List<Post>? Posts {  get; set; }
        //public List<Genre>? Genres {  get; set; }
        //public List<Author>? Authors {  get; set; }

        #region Post Bilgileri
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreateDate { get; set; }
        #endregion

        #region Yazar Bilgileri
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string FullName => $"{AuthorFirstName} {AuthorLastName}";
        public string UserImagePath { get; set; }
        #endregion
    }
}
