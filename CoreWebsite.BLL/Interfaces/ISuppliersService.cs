using System.Collections.Generic;
using System.Threading.Tasks;
using CoreWebsite.Data.Models;

namespace CoreWebsite.BLL.Interfaces
{
    public interface ISuppliersService
    {
        Task<IEnumerable<Supplier>> GetAllAsync();
    }
}