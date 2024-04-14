using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BookStore.View.User;

namespace BookStore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_getStarted_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            UserControl LoginView = new Login();
            this.Controls.Add(LoginView);
            LoginView.Show();
        }
    }
}
