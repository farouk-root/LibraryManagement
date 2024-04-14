using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BookStore.Model.Library;
using BookStore.Controller;
using BookStore.Model.User;

namespace BookStore.View.BookStore_Admin_
{
    public partial class LibrariesView : UserControl
    {
        LibraryController libraryController = new LibraryController();
        private BindingSource LibrariesBindingSource = new BindingSource();
        private UserModel userData = new UserModel();
        public LibrariesView(UserModel userData)
        {
            InitializeComponent();
            this.userData = userData;
            label1.Text = userData.Username;
            LibrariesBindingSource.DataSource = null;
            dataGridView_libraries.DataSource = LibrariesBindingSource;
            LoadData();
        }
        private void LoadData()
        {
            List<LibraryModel> products = libraryController.GetAllLibraries();
            LibrariesBindingSource.DataSource = products;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            string nameLib = textBox_NameLIB.Text;
            string locationLib = textBox_LocationLIB.Text;
            string contactLib = textBox_PhoneLIB.Text;
            string emailLib = textBox_EmailLIB.Text;
            LibraryModel newlibrary = new LibraryModel(nameLib, locationLib, emailLib, contactLib);
            if (libraryController.AddLibrary(newlibrary))
            {
                Console.WriteLine("Library Added succefully !");
            }
            else Console.WriteLine("Echec error");
        }
    }
}
