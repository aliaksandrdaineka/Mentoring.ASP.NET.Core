using CoreWebsite.BLL.Mapping.Interfaces;
using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Data.Models;

namespace CoreWebsite.BLL.Mapping
{
    public class ProductDtoMapper : IProductDtoMapper
    {
        private readonly ICategoryDtoMapper _categoryDtoMapper;
        private readonly ISupplierDtoMapper _supplierDtoMapper;

        public ProductDtoMapper(ICategoryDtoMapper categoryDtoMapper, ISupplierDtoMapper supplierDtoMapper)
        {
            _categoryDtoMapper = categoryDtoMapper;
            _supplierDtoMapper = supplierDtoMapper;
        }

        public ProductDto MapToDto(Product item)
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
                UnitPrice = item.UnitPrice,

                Category = _categoryDtoMapper.MapToDto(item.Category),
                Supplier = _supplierDtoMapper.MapToDto(item.Supplier)
            };
        }

        public Product MapToEntity(ProductDto item)
        {
            if (item == null)
                return null;

            return new Product
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
            };
        }
    }
}
