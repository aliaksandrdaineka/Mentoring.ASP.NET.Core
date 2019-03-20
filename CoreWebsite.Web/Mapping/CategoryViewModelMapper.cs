using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Web.Mapping.Interfaces;
using CoreWebsite.Web.ViewModels;

namespace CoreWebsite.Web.Mapping
{
    public class CategoryViewModelMapper : ICategoryViewModelMapper
    {
        public CategoryViewModel MapToViewModel(CategoryDto item)
        {
            if (item == null)
                return null;

            return new CategoryViewModel
            {
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName,
                Description = item.Description
            };
        }

        public CategoryDto MapToDto(CategoryViewModel item)
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
    }
}
