using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interface;
using Entities;

namespace DAL.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}
