using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductCategory.Infrastructure.DAOs
{
    /// <summary>
    /// A data access object interface for connecting to database.
    /// </summary>
    /// <typeparam name="T">Model that we want to use for database processes.</typeparam>
    public interface IDataDao<T>
    {
        Task<T> GetAsync(string id);

        Task InsertAsync(T model);

        Task UpdateAsync(string id, T model);

        Task DeleteAsync(string id);

        Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> query);

        Task<IEnumerable<T>> GetAllAsync();
    }
}
