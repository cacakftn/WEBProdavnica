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
    public class UserRepository : IUserRepository
    {
        public bool Add(User item)
        {
            using(SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "INSERT INTO Users(FirstName,LastName,Email, PasswordHash,Status,IdRole) VALUES(@FirstName,@LastName,@Email, @PasswordHash,@Status,@IdRole)";
                
                cmd.Parameters.AddWithValue("@FirstName", item.FirstName);
                cmd.Parameters.AddWithValue("@LastName", item.LastName);
                cmd.Parameters.AddWithValue("@Email", item.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", item.PasswordHash);
                cmd.Parameters.AddWithValue("@Status", item.Status);
                cmd.Parameters.AddWithValue("@IdRole", item.IdRole);
                   
                return cmd.ExecuteNonQuery() > 0;
                    
            }
        }

        public bool Delete(int Id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "DELETE FROM Users WHERE IdUser=@x";

                cmd.Parameters.AddWithValue("@x", Id);
          

                return cmd.ExecuteNonQuery() > 0;

            }
        }

        public User Get(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Users WHERE IdUser=@x";

                cmd.Parameters.AddWithValue("@x", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    User user = new User();
                    user.IdUser = reader.GetInt32(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);
                    user.Email = reader.GetString(3);
                    user.PasswordHash = reader.GetString(4);
                    user.Status = reader.GetBoolean(5);
                    user.CreatedDate = reader.GetDateTime(6);
                    user.IdRole = reader.GetInt32(7);
                    return user;
                }
                return new User();
            }
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
