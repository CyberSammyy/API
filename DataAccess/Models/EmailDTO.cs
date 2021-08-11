using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class EmailDTO
    {
        [Key]
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
        public string ConfirmationMessage { get; set; }
    }
}
