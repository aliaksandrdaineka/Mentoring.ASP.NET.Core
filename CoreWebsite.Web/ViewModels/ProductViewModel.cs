using System.ComponentModel.DataAnnotations;
using CoreWebsite.Data.Models;

namespace CoreWebsite.Web.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public int ProductId { get; set; }

        [StringLength(40), Required]
        [Display(Name="Product Name")]
        public string ProductName { get; set; }

        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }

        [StringLength(20)]
        [Display(Name = "Quantity per unit")]
        public string QuantityPerUnit { get; set; }

        [Display(Name = "Unit price")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Units in stock")]
        public short? UnitsInStock { get; set; }

        [Display(Name = "Units on order")]
        public short? UnitsOnOrder { get; set; }

        [Display(Name = "Reorder level")]
        public short? ReorderLevel { get; set; }

        [Required]
        public bool Discontinued { get; set; }

        public Category Category { get; set; }

        public Supplier Supplier { get; set; }
    }
}