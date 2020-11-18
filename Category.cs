using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryADONET
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public void Add(Category category)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Categories VALUES (@CategoryName)"))
            {
                cmd.Parameters.AddWithValue("CategoryName", category.CategoryName);
                VTYS.SqlExecuteNonQuery(cmd);
            }
        }
        public Category GetById()
        {
            return null;
        }
        public void Update(Category category)
        {

        }
        public void Delete(Category category)
        {

        }
        public List<Category> GetAll()
        {
            using (SqlCommand cmd = new SqlCommand("Select * from Categories"))
            {
                var categoryList = new List<Category>();
                
                SqlDataReader reader = VTYS.SqlExecuteReader(cmd);
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
        }
    }
}
