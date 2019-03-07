using System.ComponentModel.DataAnnotations;

namespace CoreWebsite.Data.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [StringLength(15), Required]
        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}