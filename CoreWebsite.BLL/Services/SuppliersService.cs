using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.BLL.Interfaces;
using CoreWebsite.BLL.Mapping.Interfaces;
using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Data.Interfaces;
using CoreWebsite.Data.Models;

namespace CoreWebsite.BLL.Services
{
    public class SuppliersService : ISuppliersService
    {
        private readonly IRepository<Supplier> _suppliersRepository;
        private readonly ISupplierDtoMapper _supplierMapper;

        public SuppliersService(IRepository<Supplier> suppliersRepository, ISupplierDtoMapper supplierMapper)
        {
            _suppliersRepository = suppliersRepository;
            _supplierMapper = supplierMapper;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllAsync()
        {
            var suppliers = await _suppliersRepository.GetAllAsync();
            return suppliers.Select(x => _supplierMapper.MapToDto(x));
        }
    }
}