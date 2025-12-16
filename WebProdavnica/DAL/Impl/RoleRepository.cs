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
    public class RoleRepository : IRoleRepository
    {
        public bool Add(Role item)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "INSERT INTO Roles(Name) VALUES(@Name)";

                cmd.Parameters.AddWithValue("@Name", item.Name);

                return cmd.ExecuteNonQuery() > 0;

            }
        }

        public bool Delete(int Id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "DELETE FROM Roles WHERE IdRole=@x";

                cmd.Parameters.AddWithValue("@x", Id);


                return cmd.ExecuteNonQuery() > 0;

            }
        }

        public Role Get(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Roles WHERE IdRole=@x";

                cmd.Parameters.AddWithValue("@x", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Role role = new Role();
                    role.IdRole = reader.GetInt32(0);
                    role.Name = reader.GetString(1);
                    return role; 
                }
                return new Role();
            }
        }

        public List<Role> GetAll()
        {
            List<Role> list = new List<Role>();
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Roles";

            
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Role role = new Role();
                    role.IdRole = reader.GetInt32(0);
                    role.Name = reader.GetString(1);
                    list.Add(role);
                }
                return list;
            }
        }

        public bool Update(Role item)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "UPDATE Roles SET Name=@Name WHERE IdRole=@Id";

                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Id", item.IdRole);
                return cmd.ExecuteNonQuery() > 0;

            }
        }
    }
}
