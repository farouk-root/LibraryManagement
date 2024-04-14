using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BookStore.Model.User;

namespace BookStore.View.BookStore_Admin_
{
    public partial class HomeView : UserControl
    {
        private UserModel userData = new UserModel();
        public HomeView(UserModel userData)
        {
            InitializeComponent();
            this.userData = userData;
            label1.Text = userData.Username;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            UserControl usersView = new UsersView(userData);
            this.Controls.Add(usersView);
            usersView.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            UserControl librariesView = new LibrariesView(userData);
            this.Controls.Add(librariesView);
            librariesView.Show();
        }
    }
}
