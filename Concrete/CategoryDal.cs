using LibraryADONET.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LibraryADONET.Concrete
{
    public class CategoryDal : ICategoryDal
    {
        public void Add(Category entity)
        {
            using (SqlCommand cmd  = new SqlCommand("INSERT INTO Categories(CategoryName) VALUES (@CategoryName)"))
            {
                cmd.Parameters.AddWithValue("CategoryName", entity.CategoryName);
                VTYS.SqlExecuteNonQuery(cmd);
            }
        }

        public void Delete(Category entity)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Categories WHERE CategoryId = @CategoryId"))
            {
                cmd.Parameters.AddWithValue("CategoryId", entity.CategoryId);
                VTYS.SqlExecuteNonQuery(cmd);
            }
        }

        public List<Category> GetAll()
        {
            var categoryList = new List<Category>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Categories");
            var reader = VTYS.SqlExecuteReader(cmd);

            while (reader.Read())
            {
                Category category = new Category
                {
                    CategoryId = Convert.ToInt32(reader[0]),
                    CategoryName = reader[1].ToString()
                };

                categoryList.Add(category);
            }
            return categoryList;
        }

        public Category GetById(int id)
        {
            Category category = new Category();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Categories WHERE CategoryId= @CategoryId");
            cmd.Parameters.AddWithValue("CategoryId", id);
            
            var reader = VTYS.SqlExecuteReader(cmd);

            while (reader.Read())
            {
                category.CategoryId = Convert.ToInt32(reader[0]);
                category.CategoryName = reader[1].ToString();
            }
            return category;
        }

        public void Update(Category entity)
        {
            using (SqlCommand cmd = 
                new SqlCommand("UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryId = @CategoryId"))
            {
                cmd.Parameters.AddWithValue("CategoryName", entity.CategoryName);
                cmd.Parameters.AddWithValue("CategoryId", entity.CategoryId);
                VTYS.SqlExecuteNonQuery(cmd);
            }
        }
    }
}
