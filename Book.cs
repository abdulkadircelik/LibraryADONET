using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryADONET
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int CategoryId { get; set; }

        public Book()
        {

        }

        public Book(int id, string title, string description, int year, int categoryId)
        {
            Id = id;
            Title = title;
            Description = description;
            Year = year;
            CategoryId = categoryId;
        }

        public override string ToString()
        {
            return $"{Title}";
        }

       

        public List<Book> GetAll()
        {
            var bookList = new List<Book>();
            SqlCommand cmd = new SqlCommand("Select * from Books");

            SqlDataReader reader = VTYS.SqlExecuteReader(cmd);
            while (reader.Read())
            {
                Book book = new Book
                {
                    Id = Convert.ToInt32(reader[0]),
                    Title = reader[1].ToString(),
                    Description = reader[2].ToString(),
                    Year = int.Parse(reader[3].ToString()),
                    CategoryId = Convert.ToInt32(reader[4])
                };

                bookList.Add(book);
            }
            return bookList;
        }

        public Book GetById()
        {
            return null;
        }

        public void Update()
        {

        }

        public void Delete()
        {

        }

        public void Add(Book book)
        {
            using (SqlCommand cmd =
                   new SqlCommand("INSERT INTO Books (Title,Description, Year, CategoryId) VALUES (@Title,@Description, @Year, @CategoryId)"))
            {
                cmd.Parameters.AddWithValue("Title", book.Title);
                cmd.Parameters.AddWithValue("Description", book.Description);
                cmd.Parameters.AddWithValue("Year", book.Year);
                cmd.Parameters.AddWithValue("CategoryId", book.CategoryId);
                VTYS.SqlExecuteNonQuery(cmd);
            }
        }
    }
}
