using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class HashSettings
    {
        public string PasswordSalt { get; set; }
        public int IterationCount { get; set; }
        public int NumberBytesRequested { get; set; }
    }
}
