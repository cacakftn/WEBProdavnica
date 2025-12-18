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
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IUserRepository userRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly IOrderItemRepository orderItemRepository;

        public OrderBusiness(IUserRepository userRepository, IOrderRepository orderRepository, IProductRepository productRepository, IOrderItemRepository orderItemRepository)
        {
            this.userRepository = userRepository;
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.orderItemRepository = orderItemRepository;
        }

        public ResultWrapper Add(int userId, int productId, int quantity)
        {
            if (quantity <= 0)
            {
                return new ResultWrapper
                {
                    Message = "Kolicina mora biti veca od 0",
                    Status = false
                };
            }
            User user = userRepository.Get(userId);
            if (user == null)
            {
                return new ResultWrapper
                {
                    Message = "Korisnik ne postoji",
                    Status = false
                };
            }
            var product = productRepository.Get(productId);
            if (product == null)
            {
                return new ResultWrapper
                {
                    Message = "Proizvod ne postoji",
                    Status = false
                };
            }
            if (product.Count < quantity)
            {
                return new ResultWrapper
                {
                    Message = "Nema dovoljno proizvoda",
                    Status = false
                };
            }
            int orginalniCount = product.Count;
            bool productUpdate = false;
            try
            {
                product.Count -= quantity;
                productRepository.Update(product);
                productUpdate = true;

                Order order = orderRepository.GetOrderByUser(userId);

                if (order == null)
                {
                    order = new Order();
                    order.IdUser = userId;
                    order.TotalPrice = 0M;
                    orderRepository.Add(order);
                    order = orderRepository.GetOrderByUser(userId);
                }
                var item = orderItemRepository.GetByOrderAndProduct(order.IdOrder, productId);
                if (item == null)
                {
                    item = new OrderItem();
                    item.IdOrder = order.IdOrder;
                    item.IdProduct = product.IdProduct;
                    item.Quantity = quantity;
                    item.UnitPrice = product.Price;
                    orderItemRepository.Add(item);
                }
                else
                {
                    item.Quantity += quantity;
                    orderItemRepository.Update(item);
                }
                order.TotalPrice += product.Price * quantity;
                orderRepository.Update(order);
                return new ResultWrapper
                {
                    Status = true,
                    Message = "Uspesno"
                };
            }
            catch (Exception ex)
            {
                try
                {
                    if (productUpdate)
                    {
                        product.Count = orginalniCount;
                        productRepository.Update(product);

                    }
                }
                catch
                {
                    //loger
                }

            }
            return new ResultWrapper();
        }
    }
}
