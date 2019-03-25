using Microsoft.AspNetCore.Http;

namespace CoreWebsite.Web.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public IFormFile Picture { get; set; }
    }
}