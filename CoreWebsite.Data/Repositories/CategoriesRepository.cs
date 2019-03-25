using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CoreWebsite.Data.Interfaces;
using CoreWebsite.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebsite.Data.Repositories
{
    public class CategoriesRepository : IRepository<Category>
    {
        private readonly DataContext _context;
        public CategoriesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Category> FindAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<Category> CreateAsync(Category item)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateAsync(Category item)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Category item)
        {
            throw new NotImplementedException();
        }
    }
}
