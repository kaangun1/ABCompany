using AB.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AB.Entities.Models.Concrete
{
    public class Product :BaseEntity
    {
        public string ProductName { get; set; }
        public string? ProductCode { get; set; }
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public decimal  Amount { get; set; }

        #region Unite 1-N
        public Unite Unite { get; set; }
        public int UniteId { get; set; }
        #endregion

        #region Category N-N

        public ICollection<Category> Categories { get; set; } = new List<Category>();

        #endregion
    }
}
