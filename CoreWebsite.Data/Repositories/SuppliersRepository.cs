using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreWebsite.Data.Interfaces;
using CoreWebsite.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebsite.Data.Repositories
{
    public class SuppliersRepository : IRepository<Supplier>
    {
        private readonly DataContext _context;
        public SuppliersRepository(DataContext context)
        {
            _context = context;
        }

        public Task<Supplier> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public Task<IEnumerable<Supplier>> FindAsync(Expression<Func<Supplier, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<Supplier> CreateAsync(Supplier item)
        {
            throw new NotImplementedException();
        }

        public Task<Supplier> UpdateAsync(Supplier item)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Supplier item)
        {
            throw new NotImplementedException();
        }
    }
}