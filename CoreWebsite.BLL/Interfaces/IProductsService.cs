using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreWebsite.BLL.Models;
using CoreWebsite.Data.Models;

namespace CoreWebsite.BLL.Interfaces
{
    public interface IProductsService
    {
        Task<Product> FindAsync(int id);

        Task<Product> CreateAsync(Product item);

        Task<Product> UpdateAsync(Product item);

        Task RemoveAsync(Product item);

        Task<IEnumerable<Product>> GetAllAsync();

        Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> expression);

        Task<IEnumerable<Product>> SearchAsync(ProductSearchModel searchModel = null);
    }
}