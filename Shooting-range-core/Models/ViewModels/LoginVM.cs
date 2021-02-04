using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shooting_range_core.Models.ViewModels
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string  Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("حفظ")]
        public bool RememberMe { get; set; }

    }
}
