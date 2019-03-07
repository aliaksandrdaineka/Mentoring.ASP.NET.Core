using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreWebsite.Data.Interfaces;
using CoreWebsite.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebsite.Data.Repositories
{
    public class ProductsRepository : IRepository<Product>
    {
        private readonly DataContext _context;
        public ProductsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Product> FindAsync(int id)
        {
            return await _context.Products.Include(x => x.Category).Include(x => x.Supplier).FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(x => x.Category).Include(x => x.Supplier).ToListAsync();
        }

        public async Task<Product> CreateAsync(Product item)
        {
            await _context.Products.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Product> UpdateAsync(Product item)
        {
            _context.Attach(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> expression)
        {
            return await _context.Products.Include(x => x.Category).Include(x => x.Supplier).Where(expression).ToListAsync();
        }

        public async Task RemoveAsync(Product item)
        {
            var product = await _context.Products.FindAsync(item.ProductId);
            if (product == null) return;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}