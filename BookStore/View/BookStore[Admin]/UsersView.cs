using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BookStore.Model.User;
using BookStore.Controller;
using BookStore.Model.Library;
using System.Text.RegularExpressions;

namespace BookStore.View.BookStore_Admin_
{
    public partial class UsersView : UserControl
    {
        private UserModel userData = new UserModel();
        private BindingSource UsersBindingSource = new BindingSource();
        UserController userController = new UserController();
        LibraryController libraryController = new LibraryController();
        //private UserModel usergetData = null;
        long IDuser = 0;
        public UsersView(UserModel userData)
        {
            InitializeComponent();
            this.userData = userData;
            label1.Text = userData.Username;
            UsersBindingSource.DataSource = null;
            dataGridView_Users.DataSource = UsersBindingSource;
            LoadData();
            FillLibrariesComboBox();
            comboBox_Libraries.SelectedIndex = 0;
            comboBox_Role.SelectedIndex = 0;
        }
        private void FillLibrariesComboBox()
        {


            List<LibraryModel> libraries = libraryController.GetAllLibraries();

            comboBox_Libraries.Items.Clear();

            foreach (LibraryModel library in libraries)
            {
                comboBox_Libraries.Items.Add(library.Name); // Assuming ShopModel has a Name property
            }
        }
        private void LoadData()
        {
            List<UserModel> products = userController.GetAllLibraryManagers();
            UsersBindingSource.DataSource = products;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            UserControl homeView = new HomeView(userData);
            this.Controls.Add(homeView);
            homeView.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            UserControl librariesView = new LibrariesView(userData);
            this.Controls.Add(librariesView);
            librariesView.Show();
        }
        private void FillProductDetails(UserModel u)
        {
            //textBox_nameP.Text = p.NOMPRODUIT;
            //textBox_priceP.Text = p.PRIXPRODUIT.ToString();
            //comboBox_Stocks.Text = stockController.GetStockByID(p.IDSTOCK).LOCATIONSTOCK;
            //comboBox_categoryP.Text = p.CATEGORYPRODUIT;
            //richTextBox_decriptionP.Text = p.DESCRIPTIONPRODUIT;

            textBox_username.Text= u.Username;
             textBox_password.Text= u.Password;
             textBox_Email.Text = u.Email;
             textBox_Contact.Text = u.Contact;
             comboBox_Role.Text = u.Role;
             comboBox_Libraries.Text = libraryController.GetLibraryById(u.IDLIB).Name; 
            //long library = libraryController.GetLibraryByName(comboBox_Libraries.Text).IDLIB;

        }

        public void ClearInput()
        {
            textBox_username.Text = "";
            textBox_password.Text = "";
            textBox_Email.Text = "";
            textBox_Contact.Text = "";
            IDuser = 0;
        }
        private void dataGridView_Users_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView_Users.Rows[e.RowIndex];
                IDuser = Convert.ToInt64(row.Cells[0].Value);
                
                //Console.WriteLine(IDuser);
                UserModel getUserdata = userController.GetLibraryManagerById(IDuser);
                FillProductDetails(getUserdata);
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            bool isValid = true; // Flag to track validation status
            string name = textBox_username.Text;
            string password = textBox_password.Text;
            string email = textBox_Email.Text;
            string contact = textBox_Contact.Text;
            string role = comboBox_Role.Text;
            long library = libraryController.GetLibraryByName(comboBox_Libraries.Text).IDLIB;

            if (string.IsNullOrEmpty(name))
            {
                isValid = false;
                MessageBox.Show("Please enter a username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (name.Length < 3)
            {
                isValid = false;
                MessageBox.Show("Username must be at least 3 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (string.IsNullOrEmpty(password))
            {
                isValid = false;
                MessageBox.Show("Please enter a password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (password.Length < 8)
            {
                isValid = false;
                MessageBox.Show("Password must be at least 8 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (string.IsNullOrEmpty(email))
            {
                isValid = false;
                MessageBox.Show("Please enter an email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!IsValidEmail(email)) // Assuming IsValidEmail method checks email format
            {
                isValid = false;
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // You can add similar validation for contact (e.g., only numbers)

            if (string.IsNullOrEmpty(role))
            {
                isValid = false;
                MessageBox.Show("Please select a role for the user.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Assuming comboBox_Libraries is validated indirectly by selecting from a list

            // Proceed with user creation only if validation is successful
            if (isValid)
            {
                //UserModel newuser = new UserModel("farouk", "admin", "ADMIN", "farouk", "54303898", new DateTime(2025, 06, 01));
                //userController.AddLibrary("test", "Bizerte", "lib.biz@gmail.com", "55444333");
                UserModel newuser = new UserModel(library, name, password, role, email, contact, new DateTime(2025, 06, 01));
                if (userController.SignUp(newuser))
                {
                    Console.WriteLine("user added successfully !");
                    MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInput();
                }
            }
        }
        private bool IsValidEmail(string email)
        {
            string pattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (IDuser != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this user?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (userController.DeleteLibraryManager(IDuser))
                    {
                        MessageBox.Show("User Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInput();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete user. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            bool isValid = true; // Flag to track validation status
            string name = textBox_username.Text;
            string password = textBox_password.Text;
            string email = textBox_Email.Text;
            string contact = textBox_Contact.Text;
            string role = comboBox_Role.Text;
            long library = libraryController.GetLibraryByName(comboBox_Libraries.Text).IDLIB;

            if (string.IsNullOrEmpty(name))
            {
                isValid = false;
                MessageBox.Show("Please enter a username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (name.Length < 3)
            {
                isValid = false;
                MessageBox.Show("Username must be at least 3 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (string.IsNullOrEmpty(password))
            {
                isValid = false;
                MessageBox.Show("Please enter a password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (password.Length < 8)
            {
                isValid = false;
                MessageBox.Show("Password must be at least 8 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (string.IsNullOrEmpty(email))
            {
                isValid = false;
                MessageBox.Show("Please enter an email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!IsValidEmail(email)) // Assuming IsValidEmail method checks email format
            {
                isValid = false;
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // You can add similar validation for contact (e.g., only numbers)

            if (string.IsNullOrEmpty(role))
            {
                isValid = false;
                MessageBox.Show("Please select a role for the user.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Assuming comboBox_Libraries is validated indirectly by selecting from a list

            // Proceed with user creation only if validation is successful
            if (isValid)
            {
                //UserModel newuser = new UserModel("farouk", "admin", "ADMIN", "farouk", "54303898", new DateTime(2025, 06, 01));
                //userController.AddLibrary("test", "Bizerte", "lib.biz@gmail.com", "55444333");
                UserModel newuser = new UserModel(library, name, password, role, email, contact, new DateTime(2025, 06, 01));
                if (userController.UpdateLibraryManager(IDuser ,newuser))
                {
                    Console.WriteLine("user Updated successfully !");
                    MessageBox.Show("User Updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInput();
                }
            }
        }
    }
}
