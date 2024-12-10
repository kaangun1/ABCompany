using AB.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AB.Entities.Models.Concrete
{
    public class Category :BaseEntity
    {
        public string CategoryName { get; set; }
        public string? Description { get; set; }

        public ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
