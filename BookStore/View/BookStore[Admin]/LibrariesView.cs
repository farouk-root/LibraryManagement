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
        long IDLibrary=0;
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
                LoadData();
            }
            else Console.WriteLine("Echec error");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            UserControl usersView = new UsersView(userData);
            this.Controls.Add(usersView);
            usersView.Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            UserControl homeView = new HomeView(userData);
            this.Controls.Add(homeView);
            homeView.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (IDLibrary != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this user?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (libraryController.DeleteLibrary(IDLibrary))
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

        private void button5_Click(object sender, EventArgs e)
        {
            string nameLib = textBox_NameLIB.Text;
            string locationLib = textBox_LocationLIB.Text;
            string contactLib = textBox_PhoneLIB.Text;
            string emailLib = textBox_EmailLIB.Text;
            LibraryModel newlibrary = new LibraryModel(nameLib, locationLib, emailLib, contactLib);
            if (IDLibrary != 0)
            {
                // Update scenario (confirmation before update)
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to update the library information?", "Update Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (libraryController.UpdateLibrary(IDLibrary, newlibrary))
                    {
                        MessageBox.Show("Library updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Refresh data after successful update
                        ClearInput();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update library. Please check the information and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Add scenario (confirmation before adding)
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to add a new library?", "Add Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (libraryController.AddLibrary(newlibrary))
                    {
                        MessageBox.Show("Library added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Refresh data after successful addition
                        ClearInput();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add library. Please check the information and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            
        }
        public void ClearInput()
        {
            textBox_NameLIB.Text = "";
            textBox_LocationLIB.Text = "";
            textBox_PhoneLIB.Text = "";
            textBox_EmailLIB.Text = "";
            IDLibrary = 0;
        }
        private void FillLibraryDetails(LibraryModel l)
        {
            textBox_NameLIB.Text = l.Name;
            textBox_LocationLIB.Text = l.Phone;
            textBox_PhoneLIB.Text = l.Phone;
            textBox_EmailLIB.Text = l.Email;
        }

        private void dataGridView_libraries_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView_libraries.Rows[e.RowIndex];
                IDLibrary = Convert.ToInt64(row.Cells[0].Value);

                //Console.WriteLine(IDuser);
                LibraryModel getLibdata = libraryController.GetLibraryById(IDLibrary);
                FillLibraryDetails(getLibdata);
            }
        }
    }
}
