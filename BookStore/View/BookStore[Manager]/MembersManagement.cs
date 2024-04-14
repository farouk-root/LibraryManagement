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
using BookStore.Controller;

namespace BookStore.View.BookStore_Manager_
{
    public partial class MembersManagement : UserControl
    {
        private UserModel userData = new UserModel();
        private MemberController memberController = new MemberController();
        private long IDMember=0;
        private BindingSource MembersBindingSource = new BindingSource();
        public MembersManagement(UserModel userData)
        {
            InitializeComponent();
            this.userData = userData;
            label1.Text = userData.Username;
            comboBox_Status.SelectedIndex = 0;
            MembersBindingSource.DataSource = null;
            dataGridView_Members.DataSource = MembersBindingSource;
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
        private void button_Borrowing_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            UserControl BorrowView = new BorrowingManagement(userData);
            this.Controls.Add(BorrowView);
            BorrowView.Show();
        }

        private void LoadData()
        {
            List<MemberModel> members = memberController.GetAllMembersByLibraryID(userData.IDLIB);
            MembersBindingSource.DataSource = members;
        }
        public void ClearInput()
        {
            textBox_MemberName.Text = "";
            textBox_ContactPhone.Text = "";
            textBox_Email.Text = "";
            textBox_HomeAddress.Text = "";
            IDMember = 0;
        }
        private void button_Add_Click(object sender, EventArgs e)
        {
            string memberName = textBox_MemberName.Text;
            string contactPhone = textBox_ContactPhone.Text;
            string status = comboBox_Status.Text;
            string emailMember = textBox_Email.Text;
            string addressMember = textBox_HomeAddress.Text;
            DateTime membershipEnd ;
            //= dateTimePicker_MembershipEnd.Text
            if (!DateTime.TryParse(dateTimePicker_MembershipEnd.Text, out membershipEnd))
            {
                Console.WriteLine("Invalid expiration date format. Please use a valid date format.");
            }
            DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //MemberModel newmember = new MemberModel(userData.IDLIB, "maissa", "887755", "ACTIVE","hello@gmail.com", "Bizerte", new DateTime(2025, 06, 01), new DateTime(2025, 06, 01));
            MemberModel newmember = new MemberModel(userData.IDLIB, memberName, contactPhone, status, emailMember, addressMember, currentDate,membershipEnd);
            if (memberController.AddMember(newmember))
            {
                MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearInput();
            }else
                MessageBox.Show("Failed to delete user. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            if (IDMember != 0)
            {
                string memberName = textBox_MemberName.Text;
                string contactPhone = textBox_ContactPhone.Text;
                string status = comboBox_Status.Text;
                string emailMember = textBox_Email.Text;
                string addressMember = textBox_HomeAddress.Text;
                DateTime membershipEnd;
                //= dateTimePicker_MembershipEnd.Text
                if (!DateTime.TryParse(dateTimePicker_MembershipEnd.Text, out membershipEnd))
                {
                    Console.WriteLine("Invalid expiration date format. Please use a valid date format.");
                }
                DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                //MemberModel newmember = new MemberModel(userData.IDLIB, "maissa", "887755", "ACTIVE","hello@gmail.com", "Bizerte", new DateTime(2025, 06, 01), new DateTime(2025, 06, 01));
                MemberModel newmember = new MemberModel(userData.IDLIB, memberName, contactPhone, status, emailMember, addressMember, currentDate, membershipEnd);
                if (memberController.UpdateMember(newmember,IDMember))
                {
                    MessageBox.Show("Member updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInput();
                }
                else
                    MessageBox.Show("Failed to update Member. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (IDMember != 0)
            {
                if (memberController.DeleteMember(IDMember))
                {
                    MessageBox.Show("Member Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInput();
                }
                else
                    MessageBox.Show("Please Click to select a Member from table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Failed to update Member. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void FillProductDetails(MemberModel m)
        {
            textBox_MemberName.Text = m.Name;
            textBox_ContactPhone.Text = m.ContactPhone;
            textBox_Email.Text = m.Email;
            textBox_HomeAddress.Text = m.HomeAddress;
            comboBox_Status.Text = m.Status;
            dateTimePicker_MembershipEnd.Text = m.MembershipExpiration.ToString();
        }

        private void dataGridView_Members_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView_Members.Rows[e.RowIndex];
                IDMember = Convert.ToInt64(row.Cells[0].Value);
                MemberModel getUserdata = memberController.GetMemberByID(IDMember);
                FillProductDetails(getUserdata);
            }

        }



    }
}
