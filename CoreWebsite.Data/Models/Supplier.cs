using System.ComponentModel.DataAnnotations;

namespace CoreWebsite.Data.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [StringLength(40), Required]
        public string CompanyName { get; set; }
    }
}