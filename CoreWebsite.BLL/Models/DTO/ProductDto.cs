using System;
using System.Collections.Generic;
using System.Text;
using CoreWebsite.Data.Models;

namespace CoreWebsite.BLL.Models.DTO
{
    public class ProductDto : BaseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public CategoryDto Category { get; set; }

        public SupplierDto Supplier { get; set; }
    }
}
