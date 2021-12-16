using applicationChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace applicationChallenge.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
    }
}
