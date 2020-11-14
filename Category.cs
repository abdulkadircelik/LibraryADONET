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
    }
}
