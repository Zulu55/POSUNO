using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POSUNO.Api.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(20)]
        public string Phonenumber { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [JsonIgnore]
        public User User { get; set; }
    }
}
