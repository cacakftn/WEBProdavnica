using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using Core.Result;
using DAL.Abstract;
using Entities;

namespace BusinessLayer.Impl
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository productRepository;
        public ProductBusiness(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ResultWrapper Add(Product product)
        {
            if (productRepository.Add(product) == true)
                return new ResultWrapper
                {
                    Message = "Proizvod je uspesno doddat",
                    Status = true
                };
            return new ResultWrapper
            {
                Message = "Greska",
                Status = false
            };
        }

        public ResultWrapper Delete(Product product)
        {
            if (productRepository.Delete(product.IdProduct) == true)
                return new ResultWrapper
                {
                    Message = "Proizvod je uspesno obrisan",
                    Status = true
                };
            return new ResultWrapper
            {
                Message = "Greska",
                Status = false
            };
        }

        public Product Get(int id)
        {
            return productRepository.Get(id);
        }

        public List<Product> GetAll()
        {
            if (productRepository.GetAll().Count == 0)
            {
                return new();
            }
            return productRepository.GetAll();
        }

        public ResultWrapper Update(Product product)
        {
            if (productRepository.Update(product) == true)
                return new ResultWrapper
                {
                    Message = "Proizvod je uspesno izmenjen",
                    Status = true
                };
            return new ResultWrapper
            {
                Message = "Greska",
                Status = false
            };
        }
    }
}
