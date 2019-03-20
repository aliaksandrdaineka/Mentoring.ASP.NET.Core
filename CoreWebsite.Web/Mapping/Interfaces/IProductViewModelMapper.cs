using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Web.ViewModels;

namespace CoreWebsite.Web.Mapping.Interfaces
{
    public interface IProductViewModelMapper
    {
        ProductViewModel MapToViewModel(ProductDto item);
        ProductDto MapToDto(ProductViewModel item);
    }
}