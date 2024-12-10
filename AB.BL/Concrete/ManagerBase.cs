using AB.BL.Abtract;
using AB.DAL.GenericRepository.Concrete;
using AB.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AB.BL.Concrete
{
    public class ManagerBase<T> : Repository<T>,IManager<T> where T : BaseEntity
    {
    }
}
