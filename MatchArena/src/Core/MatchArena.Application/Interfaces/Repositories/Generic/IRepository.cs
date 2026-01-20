using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Repositories.Generic
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
    }
}
