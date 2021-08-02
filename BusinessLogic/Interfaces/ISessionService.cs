using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface ISessionService
    {
        string CreateAuthToken(UserWithRoles user);
    }
}
