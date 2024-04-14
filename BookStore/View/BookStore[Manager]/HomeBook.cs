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

namespace BookStore.View.BookStore_Manager_
{
    public partial class HomeBook : UserControl
    {
        private UserModel userData = new UserModel();
        private BindingSource BooksBindingSource = new BindingSource();
        private BookModel book = new BookModel();
        private BookController bookController = new BookController();
        private LibraryController libraryController = new LibraryController();
        public HomeBook(UserModel userData)
        {
            InitializeComponent();
            this.userData = userData;
            label1.Text = userData.Username;
            BooksBindingSource.DataSource = null;
            dataGridView_booksHome.DataSource = BooksBindingSource;
            LoadData();
            label_Welcome.Text = "Welcome To  " + libraryController.GetLibraryById(userData.IDLIB).Name;
        }
        private void LoadData()
        {
            List<BookModel> Books = bookController.GetAllBooks();
            BooksBindingSource.DataSource = Books;
        }
        private void button_BookManagement_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            UserControl BooksView = new ManageBooks(userData);
            this.Controls.Add(BooksView);
            BooksView.Show();
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

    }
}
