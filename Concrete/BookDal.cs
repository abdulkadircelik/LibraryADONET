using LibraryADONET.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace LibraryADONET.Concrete
{
    public class BookDal : IBookDal
    {
        // public delegate void BookAddedEventHandler(object source, BookEventHandler args)
        public EventHandler<BookEventHandler> BookAdded;

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
                OnBookAdded(book);
            }
        }

        protected virtual void OnBookAdded(Book book)
        {
            if (BookAdded != null)
                BookAdded(this, new BookEventHandler() {Book = book});
        }

        public void Delete(Book book)
        {
            using (SqlCommand cmd =
                new SqlCommand("DELETE FROM Books where Id = @Id"))
            {
                cmd.Parameters.AddWithValue("Id", book.Id);
                VTYS.SqlExecuteNonQuery(cmd);
            }
        }

        public void Update(Book book)
        {
            using (SqlCommand cmd =
                new SqlCommand("UPDATE Books set Title = @Title, Description = @Description, Year = @Year, CategoryId = @CategoryId where Id = @Id"))
            {
                cmd.Parameters.AddWithValue("Id", book.Id);
                cmd.Parameters.AddWithValue("Title", book.Title);
                cmd.Parameters.AddWithValue("Description", book.Description);
                cmd.Parameters.AddWithValue("Year", book.Year);
                cmd.Parameters.AddWithValue("CategoryId", book.CategoryId);
                VTYS.SqlExecuteNonQuery(cmd);
            }
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

        public Book GetById(int id)
        {
            SqlCommand cmd = new SqlCommand("Select * from Books where Id = @Id");
            cmd.Parameters.AddWithValue("Id", id);

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

                return book;
            }
            return null;
        }

        public List<Book> GetByCategoryId(int id)
        {
            var query = (from book in GetAll()
                         where book.Id == id
                         select book);
            return query.ToList();
        }
    }
}
