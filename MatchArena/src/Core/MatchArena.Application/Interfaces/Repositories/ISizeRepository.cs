using MatchArena.Application.Interfaces.Repositories.Generic;
using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Size = MatchArena.Domain.Entities.Size;

namespace MatchArena.Application.Interfaces.Repositories
{
    public interface ISizeRepository:IRepository<Size>
    {
    }
}
