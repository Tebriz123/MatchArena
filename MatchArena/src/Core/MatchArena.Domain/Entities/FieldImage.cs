using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class FieldImage:BaseEntity
    {
        public string ImageUrl { get; set; }
        public long FieldId { get; set; }
        public Field Field { get; set; }

    }
}
