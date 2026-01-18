using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class FieldApproval:BaseAccountableEntity
    {
        public long FieldId { get; set; }
        public Field Field { get; set; }

        public bool IsApproved { get; set; }
        public string AdminNote { get; set; }
    }
}
