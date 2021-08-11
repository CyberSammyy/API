using System.ComponentModel.DataAnnotations;

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
