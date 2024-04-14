using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BookStore.Model.User;
using BookStore.Model.Member;
using BookStore.Model.Book;
using BookStore.Model.Borrow;
using BookStore.Controller;

namespace BookStore.View.BookStore_Manager_
{
    public partial class BorrowingManagement : UserControl
    {
        private UserModel userData = new UserModel();

        private MemberController memberController = new MemberController();
        private BindingSource MembersBindingSource = new BindingSource();

        private BindingSource BooksBindingSource = new BindingSource();
        private BookController bookController = new BookController();

        private BindingSource BorrowsBindingSource = new BindingSource();
        private BorrowController borrowController = new BorrowController();

        int IDBook = 0;
        int IDMember = 0;

        private void LoadDataMembers()
        {
            List<MemberModel> borrows = memberController.GetAllMembersByLibraryID(userData.IDLIB);
            BorrowsBindingSource.DataSource = borrows;
        }
        private void LoadDataBooks()
        {
            List<BookModel> Books = bookController.GetAllBooks();
            BooksBindingSource.DataSource = Books;
        }
        private void LoadData()
        {
            List<BorrowModel> borrows = borrowController.GetAllBorrows();
            BorrowsBindingSource.DataSource = borrows;
        }
        public BorrowingManagement(UserModel userData)
        {
            InitializeComponent();
            this.userData = userData;
            label1.Text = userData.Username;

            MembersBindingSource.DataSource = null;
            dataGridView_members.DataSource = MembersBindingSource;
            LoadDataMembers();

            BooksBindingSource.DataSource = null;
            dataGridView_books.DataSource = BooksBindingSource;
            LoadDataBooks();


            BorrowsBindingSource.DataSource = null;
            dataGridView_borrows.DataSource = BorrowsBindingSource;
            LoadData();
        }

        private void button_BookManagement_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            UserControl BooksView = new ManageBooks(userData);
            this.Controls.Add(BooksView);
            BooksView.Show();
        }

        private void button_Home_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            UserControl HomeView = new HomeBook(userData);
            this.Controls.Add(HomeView);
            HomeView.Show();
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
            bool isValid = true ;
            if (IDBook == 0)
            {
                isValid = false;
                MessageBox.Show("Please Select a book!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (isValid && IDMember == 0)
            {
                MessageBox.Show("Please Select a Member to borrow!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (isValid)
            {
                BorrowModel borrow = new BorrowModel(IDMember,IDBook,new DateTime(2025, 06, 01),new DateTime(2025, 06, 01),"Active");
                if (borrowController.AddBorrow(borrow))
                {
                    LoadData();
                    MessageBox.Show("Member added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button_Update_Click(object sender, EventArgs e)
        {

        }

        private void button_Delete_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_books_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView_books.Rows[e.RowIndex];
                IDBook = (int)Convert.ToInt64(row.Cells[0].Value);

                //Console.WriteLine(IDuser);
                BookModel getUserdata = bookController.GetBookByIdAndLibraryId(IDBook, userData.IDLIB);
                textBox_BookTitle.Text = getUserdata.Title;
                //FillBookDetails(getUserdata);
                Console.WriteLine(IDBook);
            }
        }

        private void dataGridView_members_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView_members.Rows[e.RowIndex];
                IDMember = (int)Convert.ToInt64(row.Cells[0].Value);

                //Console.WriteLine(IDuser);
                MemberModel getUserdata = memberController.GetMemberByID(IDMember);
                textBox_Member.Text = getUserdata.Name;
                Console.WriteLine(IDMember);
                //FillBookDetails(getUserdata);
            }
        }

        private void dataGridView_borrows_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
