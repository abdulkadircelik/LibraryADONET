using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryADONET.Concrete
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

       
    }
}
