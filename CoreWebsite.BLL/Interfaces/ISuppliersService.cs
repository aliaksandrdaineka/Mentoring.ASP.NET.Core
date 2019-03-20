using System.Collections.Generic;
using System.Threading.Tasks;
using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Data.Models;

namespace CoreWebsite.BLL.Interfaces
{
    public interface ISuppliersService
    {
        Task<IEnumerable<SupplierDto>> GetAllAsync();
    }
}