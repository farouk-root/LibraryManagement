using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using BookStore.Utils;
using BookStore.Model.Library;

namespace BookStore.Controller
{
    class LibraryController
    {
        public bool AddLibrary(LibraryModel library)
        {
            bool successful = false;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string insertQuery = "INSERT INTO LIBRARY (NAME, LOCATION, EMAIL, PHONE) VALUES (@Name, @Location, @Email, @Phone)";
                using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", library.Name);
                    command.Parameters.AddWithValue("@Location", library.Location);
                    command.Parameters.AddWithValue("@Email", library.Email);
                    command.Parameters.AddWithValue("@Phone", library.Phone);

                    int rowsAffected = command.ExecuteNonQuery();
                    successful = (rowsAffected == 1); // Successful if 1 row affected (inserted library)
                }
            }

            return successful;
        }

        public List<LibraryModel> GetAllLibraries()
        {
            List<LibraryModel> libraries = new List<LibraryModel>();

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string selectQuery = "SELECT IDLIB, NAME, LOCATION, EMAIL, PHONE FROM LIBRARY";
                using (OleDbCommand command = new OleDbCommand(selectQuery, connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long idlib = Convert.ToInt64(reader["IDLIB"]);
                            string name = reader["NAME"].ToString();
                            string location = reader["LOCATION"].ToString();
                            string email = reader["EMAIL"].ToString();
                            string phone = reader["PHONE"].ToString();

                            LibraryModel library = new LibraryModel(idlib, name, location, email, phone);
                            libraries.Add(library);
                        }
                    }
                }
            }

            return libraries;
        }

        public LibraryModel GetLibraryByName(string libraryName)
        {
            LibraryModel library = null;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string selectQuery = "SELECT TOP 1 IDLIB, NAME, LOCATION, EMAIL, PHONE FROM LIBRARY WHERE NAME = @Name";
                using (OleDbCommand command = new OleDbCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", libraryName);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long idlib = Convert.ToInt64(reader["IDLIB"]);
                            string name = reader["NAME"].ToString();
                            string location = reader["LOCATION"].ToString();
                            string email = reader["EMAIL"].ToString();
                            string phone = reader["PHONE"].ToString();

                            library = new LibraryModel(idlib, name, location, email, phone);
                        }
                    }
                }
            }

            return library;
        }

        public LibraryModel GetLibraryById(long libraryID)
        {
            LibraryModel library = null;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string selectQuery = "SELECT IDLIB, NAME, LOCATION, EMAIL, PHONE FROM LIBRARY WHERE IDLIB = @ID";
                using (OleDbCommand command = new OleDbCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", libraryID);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long idlib = Convert.ToInt64(reader["IDLIB"]);
                            string name = reader["NAME"].ToString();
                            string location = reader["LOCATION"].ToString();
                            string email = reader["EMAIL"].ToString();
                            string phone = reader["PHONE"].ToString();

                            library = new LibraryModel(idlib, name, location, email, phone);
                        }
                    }
                }
            }

            return library;
        }

        public bool UpdateLibrary(long IDLib , LibraryModel library)
        {
            bool isUpdated = false;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string updateQuery = "UPDATE LIBRARY SET NAME = @Name, LOCATION = @Location, EMAIL = @Email, PHONE = @Phone WHERE IDLIB = @Id";
                using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                {
                    
                    command.Parameters.AddWithValue("@Name", library.Name);
                    command.Parameters.AddWithValue("@Location", library.Location);
                    command.Parameters.AddWithValue("@Email", library.Email);
                    command.Parameters.AddWithValue("@Phone", library.Phone);
                    command.Parameters.AddWithValue("@Id", IDLib);

                    int rowsUpdated = command.ExecuteNonQuery();
                    isUpdated = rowsUpdated > 0; // Check if any rows were updated
                }
            }

            return isUpdated;
        }

        public bool DeleteLibrary(long libraryID)
        {
            bool isDeleted = false;

            if (libraryID != 0) // Ensure a valid library ID is provided
            {
                using (OleDbConnection connection = connectionDB.GetConnection())
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM LIBRARY WHERE IDLIB = @Id";
                    using (OleDbCommand command = new OleDbCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", libraryID);

                        int rowsDeleted = command.ExecuteNonQuery();
                        isDeleted = rowsDeleted > 0; // Check if any rows were deleted
                    }
                }
            }

            return isDeleted;
        }


    }
}
