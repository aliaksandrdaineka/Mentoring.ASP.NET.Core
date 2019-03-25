using System;
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
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> _categoriesRepository;
        private readonly ICategoryDtoMapper _categoryDtoMapper;
        private const int _pictureGarbageBytes = 78;

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

        public async Task<CategoryDto> GetByIdAsync(int categoryId)
        {
            var category = await _categoriesRepository.FindAsync(categoryId);
            return _categoryDtoMapper.MapToDto(category);
        }

        public async Task<byte[]> GetPictureAsync(int categoryId)
        {
            var category = await _categoriesRepository.FindAsync(categoryId);
            return category?.Picture?.Skip(_pictureGarbageBytes).ToArray();
        }

        public async Task UpdatePictureAsync(int categoryId, byte[] picture)
        {
            if (picture == null)
            {
                throw new ArgumentNullException();
            }
            var category = await _categoriesRepository.FindAsync(categoryId);

            var shift = new byte[_pictureGarbageBytes];
            category.Picture = shift.Concat(picture).ToArray();

            await _categoriesRepository.UpdateAsync(category);
        }
    }
}
