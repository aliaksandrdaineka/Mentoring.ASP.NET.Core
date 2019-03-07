using System.Collections.Generic;
using System.Threading.Tasks;
using CoreWebsite.BLL.Interfaces;
using CoreWebsite.Data.Interfaces;
using CoreWebsite.Data.Models;

namespace CoreWebsite.BLL.Services
{
    public class SuppliersService : ISuppliersService
    {
        private readonly IRepository<Supplier> _suppliersRepository;

        public SuppliersService(IRepository<Supplier> suppliersRepository)
        {
            _suppliersRepository = suppliersRepository;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _suppliersRepository.GetAllAsync();
        }
    }
}