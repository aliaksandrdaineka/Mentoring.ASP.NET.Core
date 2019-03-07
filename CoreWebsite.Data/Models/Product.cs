using System.ComponentModel.DataAnnotations;

namespace CoreWebsite.Data.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [StringLength(40), Required]
        public string ProductName { get; set; }

        public int SupplierId { get; set; }

        public int CategoryId { get; set; }

        [StringLength(20)]
        public string QuantityPerUnit { get; set; }

        public decimal UnitPrice { get; set; }

        public short UnitsInStock { get; set; }

        public short UnitsOnOrder { get; set; }

        public short ReorderLevel { get; set; }

        [Required]
        public bool Discontinued { get; set; }

        public Category Category { get; set; }

        public Supplier Supplier { get; set; }
    }
}