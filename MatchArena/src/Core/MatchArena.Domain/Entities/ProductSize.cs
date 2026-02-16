using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class ProductSize
    {
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public Size Size { get; set; }
        public long SizeId { get; set; }
    }
}
