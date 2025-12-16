using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Constant;
using DAL.Abstract;
using Entities;
using Microsoft.Data.SqlClient;

namespace DAL.Impl
{
    public class OrderItemRepository : IOrderItemRepository
    {
        public bool Add(OrderItem item)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "INSERT INTO OrderItems(IdOrder,IdProduct,Quantity, UnitPrice) VALUES(@IdOrder,@IdProduct,@Quantity, @UnitPrice)";

                cmd.Parameters.AddWithValue("@IdOrder", item.IdOrder);
                cmd.Parameters.AddWithValue("@IdProduct", item.IdProduct);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                cmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                return cmd.ExecuteNonQuery() > 0;

            }
        }

        public bool Delete(int Id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "DELETE FROM OrderItems WHERE IdOrderItem=@x";

                cmd.Parameters.AddWithValue("@x", Id);


                return cmd.ExecuteNonQuery() > 0;

            }
        }

        public OrderItem Get(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM OrderItems WHERE IdOrderItem=@x";

                cmd.Parameters.AddWithValue("@x", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   OrderItem item = new OrderItem();    
                   item.IdOrderItem = reader.GetInt32(0);
                    item.IdOrder = reader.GetInt32(1);
                    item.IdProduct = reader.GetInt32(2);    
                    item.Quantity = reader.GetInt32(3);
                    item.UnitPrice = reader.GetDecimal(4);
                    return item;
                }
                return new OrderItem();
            }
        }

        public List<OrderItem> GetAll()
        {
            List<OrderItem>list= new List<OrderItem>();
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM OrderItems";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    OrderItem item = new OrderItem();
                    item.IdOrderItem = reader.GetInt32(0);
                    item.IdOrder = reader.GetInt32(1);
                    item.IdProduct = reader.GetInt32(2);
                    item.Quantity = reader.GetInt32(3);
                    item.UnitPrice = reader.GetDecimal(4);
                    list.Add(item);
                }
                return list;
            }
        }

        public bool Update(OrderItem item)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "UPDATE OrderItems SET IdOrder=@IdOrder,IdProduct=@IdProduct,Quantity=@Quantity, UnitPrice=@UnitPrice WHERE IdOrderItem=@IdOrderItem";

                cmd.Parameters.AddWithValue("@IdOrder", item.IdOrder);
                cmd.Parameters.AddWithValue("@IdProduct", item.IdProduct);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                cmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                cmd.Parameters.AddWithValue("@IdOrderItem", item.IdOrderItem);
                return cmd.ExecuteNonQuery() > 0;

            }
        }
    }
}
