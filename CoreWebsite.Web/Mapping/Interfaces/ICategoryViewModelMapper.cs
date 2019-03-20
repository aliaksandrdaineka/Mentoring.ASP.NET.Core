using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Web.ViewModels;

namespace CoreWebsite.Web.Mapping.Interfaces
{
    public interface ICategoryViewModelMapper
    {
        CategoryViewModel MapToViewModel(CategoryDto item);
        CategoryDto MapToDto(CategoryViewModel item);
    }
}
