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

namespace BookStore.View.User
{
    public partial class SignUp : UserControl
    {
        UserController userController = new UserController();
        LibraryController libraryController = new LibraryController();
        public SignUp()
        {
            InitializeComponent();
            FillLibrariesComboBox();
            comboBox_Libraries.SelectedIndex = 0;
            comboBox_Role.SelectedIndex = 0;
            textBox_password.PasswordChar = '*';
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
        private void button_Submit_Click(object sender, EventArgs e)
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
                    this.Controls.Clear();
                    UserControl LoginView = new Login();
                    this.Controls.Add(LoginView);
                    LoginView.Show();
                }
            }
        }
        private bool IsValidEmail(string email)
        {
            string pattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

    }
}
