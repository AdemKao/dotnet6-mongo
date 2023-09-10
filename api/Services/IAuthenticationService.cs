using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Services;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(string email, string password, string name);
}
