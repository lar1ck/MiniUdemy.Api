using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}