using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Result;

namespace BusinessLayer.Abstract
{
    public interface IOrderBusiness
    {
        ResultWrapper Add(int userId, int productId, int quantity);
    }
}
