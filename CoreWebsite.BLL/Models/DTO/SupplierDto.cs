using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebsite.BLL.Models.DTO
{
    public class SupplierDto : BaseDto
    {
        public int SupplierId { get; set; }
        public string CompanyName { get; set; }
    }
}
