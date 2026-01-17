using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public abstract class BaseNameableEntity:BaseAccountableEntity
    {
        public string Name { get; set; }
    }
}
