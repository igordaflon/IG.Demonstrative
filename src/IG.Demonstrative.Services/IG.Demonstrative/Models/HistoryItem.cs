using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG.Demonstrative.Models
{
    public class HistoryItem
    {
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Display(Name = "Detalhes")]
        public string Description { get; set; }

        [Display(Name = "Data/Hora")]
        public DateTime DateTime { get; set; }
    }
}
