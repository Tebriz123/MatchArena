using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class SystemSetting:BaseNameableEntity
    {
        public string Value { get; set; }
    }
}
