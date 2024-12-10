using AB.BL.Abtract;
using AB.Entities.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AB.BL.Concrete
{
    public class ProductManager : ManagerBase<Product>, IProductManager
    {
        public bool CheckCodeAndName(string code, string productname)
        {
            var product = base.Get(p => p.ProductName == productname && p.ProductCode == code);
            if (product != null)
            {
                return true;
            }
            return false;


           // return base.Get(p=>p.ProductName == productname && p.ProductCode==code)==null ? false:true;

        }

        public override int Insert(Product entity)
        {
            if(!CheckCodeAndName(entity.ProductCode,entity.ProductName))
               return base.Insert(entity);
            else 
                throw new Exception(entity.ProductName + " " + entity.ProductCode + "  Sistemde kayitlidir");
        }
    }
}
