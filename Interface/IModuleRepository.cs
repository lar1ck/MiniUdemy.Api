using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Interface
{
    public interface IModuleRepository
    {
        Task<List<CModule>> GetAllAsync();
    }
}