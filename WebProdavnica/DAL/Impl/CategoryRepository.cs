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
    public class CategoryRepository : ICategoryRepository
    {
        public bool Add(Category item)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "INSERT INTO Categories(Name) VALUES(@Name)";

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
                cmd.CommandText = "DELETE FROM Categories WHERE IdCategory=@x";

                cmd.Parameters.AddWithValue("@x", Id);


                return cmd.ExecuteNonQuery() > 0;

            }
        }

        public Category Get(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Categories WHERE IdCategory=@x";

                cmd.Parameters.AddWithValue("@x", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   Category category = new Category();
                    category.IdCategory = reader.GetInt32(0);
                    category.Name = reader.GetString(1);
                    return category;
                }
                return new Category();
            }
        }

        public List<Category> GetAll()
        {
            List<Category> list = new List<Category>();
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Categories";

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Category category = new Category();
                    category.IdCategory = reader.GetInt32(0);
                    category.Name = reader.GetString(1);
                    list.Add(category);
                }
                return list;
            }
        }

        public bool Update(Category item)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DataBaseConstant.ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "UPDATE Categories SET Name=@Name WHERE IdCategory=@IdCategory";
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@IdCategory", item.IdCategory);

                return cmd.ExecuteNonQuery() > 0;

            }
        }
    }
}
