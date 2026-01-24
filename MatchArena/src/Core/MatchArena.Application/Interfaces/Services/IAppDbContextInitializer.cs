using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Services
{
    public interface IAppDbContextInitializer
    {
        Task InitializeDbContext();
        Task InitializeAdmin();
        Task InitializeRoleAsync();
    }
}
