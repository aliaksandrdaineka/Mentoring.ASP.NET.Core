using CoreWebsite.BLL.Mapping.Interfaces;
using CoreWebsite.BLL.Models.DTO;
using CoreWebsite.Data.Models;

namespace CoreWebsite.BLL.Mapping
{
    public class SupplierDtoMapper : ISupplierDtoMapper
    {
        public SupplierDto MapToDto(Supplier item)
        {
            if (item == null)
                return null;

            return new SupplierDto
            {
                SupplierId = item.SupplierId,
                CompanyName = item.CompanyName
            };
        }

        public Supplier MapToEntity(SupplierDto item)
        {
            if (item == null)
                return null;

            return new Supplier()
            {
                SupplierId = item.SupplierId,
                CompanyName = item.CompanyName
            };
        }
    }
}
