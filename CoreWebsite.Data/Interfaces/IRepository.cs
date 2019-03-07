using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreWebsite.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> FindAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(T item);
        Task RemoveAsync(T item);
    }
}