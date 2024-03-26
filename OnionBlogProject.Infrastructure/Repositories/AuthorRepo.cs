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
    public class AuthorRepo : BaseRepo<Author>, IAuthorRepo
    {
        public AuthorRepo(AppDbContext context, DbSet<Author> table) : base(context, table)
        {
        }
    }
}
