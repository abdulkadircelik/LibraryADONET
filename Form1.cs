using CsvHelper;
using LibraryADONET.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryADONET
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadBooks();
        }

        private void booksFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    var importer = new FileImporter<Book>(filePath);
                    importer.Import();
                    importer.List.ForEach(b => new BookDal().Add(b));
                }
            }

            LoadBooks();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void LoadBooks()
        {
            dgvBooks.DataSource = new BookDal().GetAll().ToList();
        }

        private void LoadCategories()
        {
            cbCategories.DataSource = new CategoryDal().GetAll().ToList();
            cbCategories.DisplayMember = "CategoryName";
            cbCategories.ValueMember = "CategoryId";
        }

        private void categoriesFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    var importer = new FileImporter<Category>(filePath);
                    importer.Import();
                    importer.List.ForEach(c => new CategoryDal().Add(c));
                }

                LoadCategories();
            }
        }

      

        private void cbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var bookList = new BookDal().GetAll();
                var categoryList = new CategoryDal().GetAll();
                dgvBooks.DataSource =
                    bookList
                    .Join(categoryList,
                    b => b.CategoryId,
                    c => c.CategoryId,
                    (b, c) => new
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Description = b.Description,
                        Year = b.Year,
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName

                    })
                    .Where(b => b.CategoryId == Convert.ToInt32(cbCategories.SelectedValue.ToString()))
                    .ToList();
            }
            catch 
            {

               
            }
            
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new BookAddForm().Show();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you want to exit?","Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            switch (result)
            {
                case DialogResult.Yes:
                    this.Close();
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
        }

        private void dgvBooks_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvBooks.Rows[e.RowIndex];
            var book = new BookDal().GetById(Convert.ToInt32(row.Cells[0].Value.ToString()));
            lblBook.Text = book.ToString();
        }

        private void lblMouse_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            lblMouse.Text = $"{e.X},{e.Y}";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string key = txtSearch.Text.ToLower();
            dgvBooks.DataSource =
                new BookDal()
                .GetAll()
                .Where(b => b.Title.ToLower()
                .Contains(key))
                .ToList();
        }
    }

    
}
