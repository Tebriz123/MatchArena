using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class Field:BaseNameableEntity
    {
        public string Address { get; set; }
        public string City { get; set; }
        public decimal PricePerHour { get; set; }
        public double Raiting { get; set; }
        public ICollection<FieldImage> Images { get; set; }
    }
}
