using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Data.Models;

namespace CoreWebsite.BLL.Mapping.Interfaces
{
    public interface IProductDtoMapper
    {
        ProductDto MapToDto(Product item);
        Product MapToEntity(ProductDto item);
    }
}
