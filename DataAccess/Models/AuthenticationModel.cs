using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.Models
{
    public class AuthenticationModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Login { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}
