using AB.DAL.GenericRepository.Abstract;
using AB.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AB.BL.Abtract
{
    public interface IManager<T> : IRepository<T> where T : BaseEntity
    {

    }
}
