using System.ComponentModel.DataAnnotations;

namespace IG.Demonstrative.Models
{
    public class ReasonModel
    {
        private const string stringLenghtErrorMessage = "O campo '{0}' pode ter, no máximo, {2} caracteres";

        [Display(Name = "Motivo")]
        [StringLength(255, ErrorMessage = stringLenghtErrorMessage)]
        public string Description { get; set; }
    }
}
