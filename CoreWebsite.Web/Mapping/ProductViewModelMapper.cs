using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Web.Mapping.Interfaces;
using CoreWebsite.Web.ViewModels;

namespace CoreWebsite.Web.Mapping
{
    public class ProductViewModelMapper : IProductViewModelMapper
    {
        public ProductViewModel MapToViewModel(ProductDto item)
        {
            if (item == null)
                return null;

            return new ProductViewModel
            {
                ProductId = item.ProductId,
                CategoryId = item.CategoryId,
                Discontinued = item.Discontinued,
                ProductName = item.ProductName,
                QuantityPerUnit = item.QuantityPerUnit,
                ReorderLevel = item.ReorderLevel,
                SupplierId = item.SupplierId,
                UnitsOnOrder = item.UnitsOnOrder,
                UnitsInStock = item.UnitsInStock,
                UnitPrice = item.UnitPrice,

                CategoryName = item.Category?.CategoryName,
                SupplierName = item.Supplier?.CompanyName
            };
        }

        public ProductDto MapToDto(ProductViewModel item)
        {
            if (item == null)
                return null;

            return new ProductDto
            {
                ProductId = item.ProductId,
                CategoryId = item.CategoryId,
                Discontinued = item.Discontinued,
                ProductName = item.ProductName,
                QuantityPerUnit = item.QuantityPerUnit,
                ReorderLevel = item.ReorderLevel,
                SupplierId = item.SupplierId,
                UnitsOnOrder = item.UnitsOnOrder,
                UnitsInStock = item.UnitsInStock,
                UnitPrice = item.UnitPrice
            };
        }
    }
}
