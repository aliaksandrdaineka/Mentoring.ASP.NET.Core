using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CoreWebsite.Api.Models
{
    public class CategoryImageUpload
    {
        public int CategoryId { get; set; }

        public IFormFile File { get; set; }
    }
}
