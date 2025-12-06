using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MiniUdemy.Api.Interface
{
    public interface IModuleRepository
    {
        Task<List<Module>> GetAllAsync();
    }
}