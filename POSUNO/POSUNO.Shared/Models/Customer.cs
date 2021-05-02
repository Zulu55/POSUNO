namespace POSUNO.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phonenumber { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public User User { get; set; }

        public bool WasSaved { get; set; }

        public bool IsEdit { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
