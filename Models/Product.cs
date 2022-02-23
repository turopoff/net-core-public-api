using System.ComponentModel.DataAnnotations;

namespace PrepTeach.Models
{
    public class Product : BaseModel
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; } = null!;

        [StringLength(500)]
        public string Quantity { get; set; } = null!;

        public decimal Price { get; set; } = 0;

        public bool IsSold { get; set; }

        [StringLength(500)]
        public string Image { get; set; } = null!;
    }
}
