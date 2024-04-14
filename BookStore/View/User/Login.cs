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
using BookStore.View.BookStore_Admin_;
using BookStore.View.BookStore_Manager_;

namespace BookStore.View.User
{
    public partial class Login : UserControl
    {
        UserController userController = new UserController();
        public UserModel userData = new UserModel();

        public Login()
        {
            InitializeComponent();
        }

        private void button_signUp_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            UserControl SignUpView = new SignUp();
            this.Controls.Add(SignUpView);
            SignUpView.Show();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            string Username = textBox_Username.Text;
            string Password = textBox_password.Text;
            UserModel user = null;
            user = userController.SignIn(Username, Password);
            if (user != null)
            {
                MessageBox.Show("Login successfully! Welcome  " + user.Username, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                userData = new UserModel(user.ID, user.IDLIB, user.Username, user.Password, user.Role, user.Email,user.Contact,user.RegistrationDate , user.LastLoginDate);
                if (user.Role == "ADMIN")
                {
                    this.Controls.Clear();
                    UserControl AdminView = new HomeView(userData);
                    this.Controls.Add(AdminView);
                    AdminView.Show();
                }
                else if (user.Role == "MANAGER")
                {
                    this.Controls.Clear();
                    UserControl HomeManagerView = new HomeBook(userData);
                    this.Controls.Add(HomeManagerView);
                    HomeManagerView.Show();
                }
            }
            //Console.WriteLine(user.Username);

        }
    }
}
