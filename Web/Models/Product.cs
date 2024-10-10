using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quntity { get; set; }
        public decimal Price { get; set; }
        public decimal Descount { get; set; }
        public decimal Total { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
