using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG.Demonstrative.Services
{
    public static class Helper
    {
        public static void ValidateAnnotations(object obj) => Validator.ValidateObject(obj, new ValidationContext(obj), true);
    }
}
