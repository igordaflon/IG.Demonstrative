using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG.Demonstrative.Models
{
    public class CustomerEditModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        [StringLength(64, ErrorMessage = "O campo '{0}' pode ter, no máximo, {1} caracteres")]
        public string Name { get; set; }
        
        [Display(Name = "Ativo")]
        public bool IsActive { get; set; }
    }
}
