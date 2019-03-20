using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreWebsite.BLL.Models;
using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Data.Models;

namespace CoreWebsite.BLL.Interfaces
{
    public interface IProductsService
    {
        Task<ProductDto> FindAsync(int id);

        Task<ProductDto> CreateAsync(ProductDto item);

        Task<ProductDto> UpdateAsync(ProductDto item);

        Task RemoveAsync(ProductDto item);

        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<IEnumerable<ProductDto>> FindAsync(Expression<Func<Product, bool>> expression);

        Task<IEnumerable<ProductDto>> SearchAsync(ProductSearchModel searchModel = null);
    }
}