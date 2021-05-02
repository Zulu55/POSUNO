using System.ComponentModel.DataAnnotations;

namespace POSUNO.Api.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public float Stock { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public User User { get; set; }
    }
}
