using System;
using System.Collections.Generic;
using System.Text;
using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Data.Models;

namespace CoreWebsite.BLL.Mapping.Interfaces
{
    public interface ICategoryDtoMapper
    {
        CategoryDto MapToDto(Category item);
        Category MapToEntity(CategoryDto item);
    }
}
