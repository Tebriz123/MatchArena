using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Domain.Entities
{
    public class Product:BaseNameableEntity
    {
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public long? CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }

    }
}
