using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebsite.BLL.Models.DTO
{
    public class CategoryDto : BaseDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
