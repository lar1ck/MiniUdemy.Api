using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MiniUdemy.Api.Dtos.Module;
using MiniUdemy.Api.Models;

namespace MiniUdemy.Api.Interface
{
    public interface IModuleRepository
    {
        Task<List<CModule>> GetAllAsync();
        Task<List<CModule>> GetUserModulesAsync(AppUser appUser);
        Task<CModule?> GetByIdAsync(int id);
        Task<CModule> CreateAsync(CModule data);
        Task<CModule?> DeleteAsync(int id, AppUser appUser);
        Task<CModule?> UpadteAsync(CModule data, int id, AppUser appUser);
    }
}