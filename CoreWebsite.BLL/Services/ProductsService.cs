using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreWebsite.BLL.Interfaces;
using CoreWebsite.BLL.Mapping.Interfaces;
using CoreWebsite.BLL.Models;
using CoreWebsite.BLL.Models.DTO;
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
        private readonly IProductDtoMapper _productMapper;
        private readonly int _maxProductsCountDefault;


        public ProductsService(DataContext context, IRepository<Product> productsRepository, ISettingsProvider settings, IProductDtoMapper productMapper)
        {
            _context = context;
            _productsRepository = productsRepository;
            _productMapper = productMapper;
            _maxProductsCountDefault = settings.GetMaximumProductsCount;
        }

        public async Task<ProductDto> CreateAsync(ProductDto item)
        {
            var product = _productMapper.MapToEntity(item);
            var savedProduct = await _productsRepository.CreateAsync(product);
            return _productMapper.MapToDto(savedProduct);
        }

        public async Task<ProductDto> UpdateAsync(ProductDto item)
        {
            var product = _productMapper.MapToEntity(item);
            var savedProduct = await _productsRepository.UpdateAsync(product);
            return _productMapper.MapToDto(savedProduct);
        }

        public async Task RemoveAsync(ProductDto item)
        {
            var product = _productMapper.MapToEntity(item);
            await _productsRepository.RemoveAsync(product);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productsRepository.FindAsync(id);
            return _productMapper.MapToDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productsRepository.GetAllAsync();
            return products.Select(x => _productMapper.MapToDto(x));
        }

        public async Task<IEnumerable<ProductDto>> FindAsync(Expression<Func<Product, bool>> expression)
        {
            var products = await _productsRepository.FindAsync(expression);
            return products.Select(x => _productMapper.MapToDto(x));
        }

        public async Task<IEnumerable<ProductDto>> SearchAsync(ProductSearchModel searchModel = null)
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

            return await query.Select(x => _productMapper.MapToDto(x)).ToListAsync();
        }
    }
}