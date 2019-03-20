using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreWebsite.BLL.Interfaces;
using CoreWebsite.BLL.Mapping.Interfaces;
using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Data.Interfaces;
using CoreWebsite.Data.Models;

namespace CoreWebsite.BLL.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> _categoriesRepository;
        private readonly ICategoryDtoMapper _categoryDtoMapper;

        public CategoriesService(IRepository<Category> categoriesRepository, ICategoryDtoMapper categoryDtoMapper)
        {
            _categoriesRepository = categoriesRepository;
            _categoryDtoMapper = categoryDtoMapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoriesRepository.GetAllAsync();
            return categories.Select(x => _categoryDtoMapper.MapToDto(x));
        }
    }
}
