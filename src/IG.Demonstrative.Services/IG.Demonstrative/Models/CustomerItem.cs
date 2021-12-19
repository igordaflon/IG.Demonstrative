using System.ComponentModel.DataAnnotations;

namespace IG.Demonstrative.Models
{
    public class CustomerItem
    {
        [Display(Name = "Id")]
        public int Id{ get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Ativo")]
        public bool IsActive { get; set; }

        [Display(Name = "Excluído")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Data de registro")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Status")]
        public string RecordStatus => IsDeleted ? "Excluído" : (IsActive ? "Ativo" : "Inativo");
    }
}
