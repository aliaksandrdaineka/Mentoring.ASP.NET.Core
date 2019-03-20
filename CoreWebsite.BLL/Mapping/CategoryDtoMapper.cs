using CoreWebsite.BLL.Mapping.Interfaces;
using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Data.Models;

namespace CoreWebsite.BLL.Mapping
{
    public class CategoryDtoMapper : ICategoryDtoMapper
    {
        public CategoryDto MapToDto(Category item)
        {
            if (item == null)
                return null;

            return new CategoryDto
            {
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName,
                Description = item.Description
            };
        }

        public Category MapToEntity(CategoryDto item)
        {
            if (item == null)
                return null;

            return new Category
            {
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName,
                Description = item.Description
            };
        }
    }
}
