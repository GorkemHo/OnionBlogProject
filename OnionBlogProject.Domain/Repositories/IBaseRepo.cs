using Microsoft.EntityFrameworkCore.Query;
using OnionBlogProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Domain.Repositories
{
    public interface IBaseRepo<T> where T : IBaseEntity
    {
        Task Create(T entity); //Metot sonuna Async eklenebilir.
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetDeault(Expression<Func<T, bool>> expression);
        Task<List<T>> GetDeaults(Expression<Func<T, bool>> expression);
        Task<bool> Any(Expression<Func<T, bool>> expression);
        Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> select,
                                                         Expression<Func<T, bool>> where,
                                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
                                                         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select,
                                                         Expression<Func<T, bool>> where,
                                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
                                                         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    }
}
