using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG.Demonstrative.Entities
{
    public class CustomerHistory
    {
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [StringLength(int.MaxValue)]
        public string? Description { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
