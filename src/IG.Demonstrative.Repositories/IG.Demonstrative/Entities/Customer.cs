using System.ComponentModel.DataAnnotations;

namespace IG.Demonstrative.Entities
{
    public class Customer
    {
        public IEnumerable<Customer> Customers { get; set; }

        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }   
        public bool IsDeleted { get; set; }   
    }
}
