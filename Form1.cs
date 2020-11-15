using CsvHelper;
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
            LoadBooks();
            LoadCategories();
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
                    importer.List.ForEach(b => new Book().Add(b));
                }
            }

            LoadBooks();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void LoadBooks()
        {
            dgvBooks.DataSource = new Book().GetAll().ToList();
        }

        private void LoadCategories()
        {
            cbCategories.DataSource = new Category().GetAll().ToList();
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
                    importer.List.ForEach(c => new Category().Add(c));
                }

                LoadCategories();
            }
        }

      

        private void cbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            var bookList = new Book().GetAll();
            var categoryList = new Category().GetAll();
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
                .Where(b=>b.CategoryId==1)
                .ToList();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new BookAddForm().Show();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadBooks();
        }
    }

    
}
