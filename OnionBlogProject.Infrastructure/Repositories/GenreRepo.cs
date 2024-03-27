using Microsoft.EntityFrameworkCore;
using OnionBlogProject.Domain.Entities;
using OnionBlogProject.Domain.Repositories;
using OnionBlogProject.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Infrastructure.Repositories
{
    public class GenreRepo : BaseRepo<Genre>, IGenreRepo
    {
        public GenreRepo(AppDbContext context) : base(context)
        {
        }
    }
}
