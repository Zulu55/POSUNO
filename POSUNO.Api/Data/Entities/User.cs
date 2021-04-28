using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POSUNO.Api.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(6)]
        public string Password { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
