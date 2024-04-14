using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BookStore.Model.User;
using BookStore.Model.Book;
using BookStore.Controller;
using BookStore.Model.Library;

namespace BookStore.View.BookStore_Manager_
{
    public partial class ManageBooks : UserControl
    {
        private UserModel userData = new UserModel();
        private BindingSource BooksBindingSource = new BindingSource();
        private BookModel book = new BookModel();
        private BookController bookController = new BookController();
        long IDbook = 0;
        LibraryController libraryController = new LibraryController();

        public ManageBooks(UserModel userData)
        {
            InitializeComponent();
            this.userData = userData;
            label1.Text = userData.Username;
            BooksBindingSource.DataSource = null;
            dataGridView_books.DataSource = BooksBindingSource;
            LoadData();
            comboBox_Genre.SelectedIndex= 0;
        }

        private void LoadData()
        {
            List<BookModel> Books = bookController.GetAllBooks();
            BooksBindingSource.DataSource = Books;
        }

        private void button_Home_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            UserControl HomeView = new HomeBook(userData);
            this.Controls.Add(HomeView);
            HomeView.Show();
        }

        private void button_Borrowing_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            UserControl BorrowView = new BorrowingManagement(userData);
            this.Controls.Add(BorrowView);
            BorrowView.Show();
        }

        private void button_MemberManagement_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            UserControl MembersManageView = new MembersManagement(userData);
            this.Controls.Add(MembersManageView);
            MembersManageView.Show();
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            string title = textBox_BookTitle.Text;
            string Author = textBox_BookAuthor.Text;
            string Genre = comboBox_Genre.Text;
            string Description = richTextBox_Description.Text;
            int nbCopies;
            int.TryParse(textBox_NBCopies.Text, out nbCopies);


            BookModel newBOOK = new BookModel(userData.IDLIB , title ,Author,Genre,Description,nbCopies,nbCopies);
            if (bookController.AddBook(newBOOK))
            {
                MessageBox.Show("Book Added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }else
                MessageBox.Show("Failed to add book. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            if (IDbook != 0)
            {
                string title = textBox_BookTitle.Text;
                string Author = textBox_BookAuthor.Text;
                string Genre = comboBox_Genre.Text;
                string Description = richTextBox_Description.Text;
                int nbCopies;
                int.TryParse(textBox_NBCopies.Text, out nbCopies);


                BookModel newBOOK = new BookModel(userData.IDLIB, title, Author, Genre, Description, nbCopies, nbCopies);
                if (bookController.UpdateBook(newBOOK , IDbook))
                {
                    MessageBox.Show("Book Updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                    MessageBox.Show("Failed to Update book. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (IDbook != 0)
            {
                if (bookController.DeleteBook(IDbook))
                {
                    MessageBox.Show("Book Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                    MessageBox.Show("Failed to Delete book. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FillBookDetails(BookModel b)
        {
            textBox_BookTitle.Text = b.Title;
            textBox_BookAuthor.Text = b.Author;
            comboBox_Genre.Text = b.Genre;
            textBox_NBCopies.Text = b.NbCopies.ToString();
            richTextBox_Description.Text = b.Description;
        }

        private void dataGridView_books_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView_books.Rows[e.RowIndex];
                IDbook = Convert.ToInt64(row.Cells[0].Value);

                //Console.WriteLine(IDuser);
                BookModel getUserdata = bookController.GetBookByIdAndLibraryId(IDbook,userData.IDLIB);
                FillBookDetails(getUserdata);
            }
        }


    }
}
