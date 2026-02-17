using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class ProductImage:BaseEntity
    {
        public string Image { get; set; }
        public bool IsPrimary { get; set; }
        public Product Product { get; set; }
        public long ProductId { get; set; }
    }
}
