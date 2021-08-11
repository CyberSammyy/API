using System;

namespace BusinessLogic.Models
{
    public class ConfirmationMessageModel
    {
        public Guid UserId { get; set; }
        public string ConfirmationMessage { get; set; }
    }
}
