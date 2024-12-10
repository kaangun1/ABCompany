using AB.Entities.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AB.BL.Abtract
{
    public interface IProductManager : IManager<Product>
    {

        public bool CheckCodeAndName(string code,string productname);
    }
}
