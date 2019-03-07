using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreWebsite.BLL.Interfaces;
using CoreWebsite.BLL.Models;
using CoreWebsite.Data;
using CoreWebsite.Data.Interfaces;
using CoreWebsite.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebsite.BLL.Services
{
    public class ProductsService : IProductsService
    {
        private readonly DataContext _context;
        private readonly IRepository<Product> _productsRepository;
        private readonly int _maxProductsCountDefault;


        public ProductsService(DataContext context, IRepository<Product> productsRepository, ISettingsProvider settings)
        {
            _context = context;
            _productsRepository = productsRepository;
            _maxProductsCountDefault = settings.GetMaximumProductsCount;
        }

        public async Task<Product> CreateAsync(Product item)
        {
            return await _productsRepository.CreateAsync(item);
        }

        public async Task<Product> UpdateAsync(Product item)
        {
            return await _productsRepository.UpdateAsync(item);
        }

        public async Task RemoveAsync(Product item)
        {
            await _productsRepository.RemoveAsync(item);
        }

        public async Task<Product> FindAsync(int id)
        {
            return await _productsRepository.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productsRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> expression)
        {
            return await _productsRepository.FindAsync(expression);
        }

        public async Task<IEnumerable<Product>> SearchAsync(ProductSearchModel searchModel = null)
        {
            IQueryable<Product> query = _context.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier);

            // searchModel params have more priority than default config value
            if (searchModel?.MaxCount > 0)
                query = query.Take(searchModel.MaxCount);
            else if (_maxProductsCountDefault > 0)
            {
                query = query.Take(_maxProductsCountDefault);
            }

            return await query.ToListAsync();
        }
    }
}