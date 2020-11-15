using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryADONET
{
    public partial class BookAddForm : Form
    {
        public BookAddForm()
        {
            InitializeComponent();
        }

        private void btnBookAdd_Click(object sender, EventArgs e)
        {
            Book book = new Book
            {
                Id = 0,
                Title = txtTtile.Text,
                Description = txtDescription.Text,
                Year = Convert.ToInt32(txtYear.Text),
                CategoryId = 1
            };

            book.Add(book);
            MessageBox.Show($"{book} has been added.", "Succed");
        }

        private void BookAddForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void LoadCategories()
        {
            cbCategory.DataSource = new Category().GetAll().ToList();
            cbCategory.DisplayMember = "CategoryName";
            cbCategory.ValueMember = "CategoryId";
        }
    }
}
