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
                cmd.CommandText = "INSERT INTO Users(FirstName,LastName,Email, PasswordHash,Status,IdRole, Username, RefreshToken, RefreshTokenExpiry) VALUES(@FirstName,@LastName,@Email, @PasswordHash,@Status,@IdRole, @Username, @RefreshToken, @RefreshTokenExpiry)";
                
                cmd.Parameters.AddWithValue("@FirstName", item.FirstName);
                cmd.Parameters.AddWithValue("@LastName", item.LastName);
                cmd.Parameters.AddWithValue("@Email", item.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", item.PasswordHash);
                cmd.Parameters.AddWithValue("@Status", item.Status);
                cmd.Parameters.AddWithValue("@IdRole", item.IdRole);
                cmd.Parameters.AddWithValue("@Username", item.Username);
                cmd.Parameters.AddWithValue("@RefreshToken", item.RefreshToken);
                cmd.Parameters.AddWithValue("@RefreshTokenExpiry", item.RefreshTokenExpiry);
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
                    user.Email = reader.GetString(3);
                    user.IdUser = reader.GetInt32(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);
                   
                    user.PasswordHash = reader.GetString(4);
                    user.Status = reader.GetBoolean(5);
                    user.CreatedDate = reader.GetDateTime(6);
                    user.IdRole = reader.GetInt32(7);
                    user.Username = reader.GetString(8);
                    user.RefreshToken = reader.GetString(9);
                    user.RefreshTokenExpiry = reader.GetDateTime(10);
                    return user;
                }
                return new User();
            }
        }

        public List<User> GetAll()
        {
            List<User> list = new List<User>();
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Users";

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    User user = new User();
                    user.Email = reader.GetString(3);
                    user.IdUser = reader.GetInt32(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);

                    user.PasswordHash = reader.GetString(4);
                    user.Status = reader.GetBoolean(5);
                    user.CreatedDate = reader.GetDateTime(6);
                    user.IdRole = reader.GetInt32(7);
                    user.Username = reader.GetString(8);
                    user.RefreshToken = reader.GetString(9);
                    user.RefreshTokenExpiry = reader.GetDateTime(10);
                    list.Add(user);
                }
                return list;
            }
        }

        public User GetByEmail(string email)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Users WHERE Email=@x";

                cmd.Parameters.AddWithValue("@x", email);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    User user = new User();
                    user.Email = reader.GetString(3);
                    user.IdUser = reader.GetInt32(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);

                    user.PasswordHash = reader.GetString(4);
                    user.Status = reader.GetBoolean(5);
                    user.CreatedDate = reader.GetDateTime(6);
                    user.IdRole = reader.GetInt32(7);
                    user.Username = reader.GetString(8);
                    user.RefreshToken = reader.GetString(9);
                    user.RefreshTokenExpiry = reader.GetDateTime(10);
                    return user;
                }
                return new User();
            }
        }

        public User GetByRefreshToken(string refreshToken)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Users WHERE RefreshToken=@x";

                cmd.Parameters.AddWithValue("@x", refreshToken);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    User user = new User();
                    user.Email = reader.GetString(3);
                    user.IdUser = reader.GetInt32(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);

                    user.PasswordHash = reader.GetString(4);
                    user.Status = reader.GetBoolean(5);
                    user.CreatedDate = reader.GetDateTime(6);
                    user.IdRole = reader.GetInt32(7);
                    user.Username = reader.GetString(8);
                    user.RefreshToken = reader.GetString(9);
                    user.RefreshTokenExpiry = reader.GetDateTime(10);
                    return user;
                }
                return new User();
            }
        }

        public User GetByUsername(string username)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Users WHERE Username=@x";

                cmd.Parameters.AddWithValue("@x", username);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    User user = new User();
                    user.Email = reader.GetString(3);
                    user.IdUser = reader.GetInt32(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);

                    user.PasswordHash = reader.GetString(4);
                    user.Status = reader.GetBoolean(5);
                    user.CreatedDate = reader.GetDateTime(6);
                    user.IdRole = reader.GetInt32(7);
                    user.Username = reader.GetString(8);
                    user.RefreshToken = reader.GetString(9);
                    user.RefreshTokenExpiry = reader.GetDateTime(10);
                    return user;
                }
                return new User();
            }
        }

        public bool Update(User item)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "UPDATE Users SET FirstName=@FirstName, LastName=@LastName, Email=@Email,PasswordHash=@PasswordHash, Status=@Status, IdRole=@IdRole,Username=@Username,RefreshToken=@RefreshToken,RefreshTokenExpiry=@RefreshTokenExpiry    WHERE IdUser=@IdUser";

                cmd.Parameters.AddWithValue("@FirstName", item.FirstName);
                cmd.Parameters.AddWithValue("@LastName", item.LastName);
                cmd.Parameters.AddWithValue("@Email", item.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", item.PasswordHash);
                cmd.Parameters.AddWithValue("@Status", item.Status);
                cmd.Parameters.AddWithValue("@IdRole", item.IdRole);
                cmd.Parameters.AddWithValue("@Username", item.Username);
                cmd.Parameters.AddWithValue("@RefreshToken", item.RefreshToken);
                cmd.Parameters.AddWithValue("@RefreshTokenExpiry", item.RefreshTokenExpiry);
                cmd.Parameters.AddWithValue("@IdUser", item.IdUser);
                return cmd.ExecuteNonQuery() > 0;

            }
        }
    }
}
