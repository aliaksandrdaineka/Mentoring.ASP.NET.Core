using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Data.Models;

namespace CoreWebsite.BLL.Interfaces
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
    }
}
