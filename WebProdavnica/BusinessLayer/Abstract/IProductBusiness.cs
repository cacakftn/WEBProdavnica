using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Result;
using Entities;

namespace BusinessLayer.Abstract
{
    public interface IProductBusiness
    {
        ResultWrapper Add(Product product);
        ResultWrapper Delete(Product product);
        List<Product> GetAll();
        Product Get(int id);
        ResultWrapper Update(Product product);

    }
}
