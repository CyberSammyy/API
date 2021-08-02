using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IHashService
    {
        string HashString(string stringToHash);
    }
}
