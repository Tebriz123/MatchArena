using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class ProductColor
    {
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public Color Color { get; set; }
        public long ColorId { get; set; }
    }
}
