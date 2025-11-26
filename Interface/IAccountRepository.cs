using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiniUdemy.Api.Dtos.Account;

namespace MiniUdemy.Api.Interface
{
    public interface IAccountRepository
    {
        Task<NewUserDto> RegisterUser();
    }
}