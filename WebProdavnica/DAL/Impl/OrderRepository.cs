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
    public class OrderRepository : IOrderRepository
    {
        public bool Add(Order item)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "INSERT INTO Orders(IdUser,TotalPrice) VALUES(@IdUser,@TotalPrice)";

                cmd.Parameters.AddWithValue("@IdUser", item.IdUser);
                cmd.Parameters.AddWithValue("@TotalPrice", item.TotalPrice);
                return cmd.ExecuteNonQuery() > 0;

            }
        }

        public bool Delete(int Id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "DELETE FROM Orders WHERE IdOrder=@IdOrder";
                cmd.Parameters.AddWithValue("@IdOrder", Id);
                return cmd.ExecuteNonQuery() > 0;

            }
        }

        public Order Get(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Orders WHERE IdOrder=@x";

                cmd.Parameters.AddWithValue("@x", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Order order = new Order();
                    order.IdOrder = reader.GetInt32(0);
                    order.IdUser = reader.GetInt32(1);
                    order.OrderDate = reader.GetDateTime(2);
                    order.TotalPrice = reader.GetDecimal(3);
                   
                }
                return new Order();
            }
        }

        public List<Order> GetAll()
        {
            List<Order> list = new List<Order>();
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Orders";

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order();
                    order.IdOrder = reader.GetInt32(0);
                    order.IdUser = reader.GetInt32(1);
                    order.OrderDate = reader.GetDateTime(2);
                    order.TotalPrice = reader.GetDecimal(3);
                    list.Add(order);
                }
                return list;
            }
        }

        public bool Update(Order item)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "UPDATE Orders SET IdUser=@IdUser,TotalPrice=@TotalPrice WHERE IdOrder=@IdOrder";

                cmd.Parameters.AddWithValue("@IdUser", item.IdUser);
                cmd.Parameters.AddWithValue("@TotalPrice", item.TotalPrice);
                cmd.Parameters.AddWithValue("@IdOrder", item.IdOrder);
                return cmd.ExecuteNonQuery() > 0;

            }
        }
    }
}
