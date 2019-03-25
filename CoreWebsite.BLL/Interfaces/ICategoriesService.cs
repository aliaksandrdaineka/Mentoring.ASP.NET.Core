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

        Task<CategoryDto> GetByIdAsync(int categoryId);

        Task<byte[]> GetPictureAsync(int categoryId);

        Task UpdatePictureAsync(int categoryId, byte[] picture);
    }
}
