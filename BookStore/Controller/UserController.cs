using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.Model.User;
using System.Data.OleDb;
using BookStore.Utils;

namespace BookStore.Controller
{
    class UserController
    {

        public Boolean SignUp(UserModel user)
        {
            //long idShop;
            //if (user.Role == "ADMIN")
            //    idShop = 1;
            //else idShop = user.IDLIB;

            //int idLIB = 2;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                //string insertQuery = "INSERT INTO SHOPUSER(ID,IDSHOP,[USERNAME], [PASSWORD], [ROLE]) VALUES(@Id,@IdShop,@Username, @Password, @Role)";
                string insertQuery = "INSERT INTO LIBMANAGERS(IDLIB ,[USERNAME] ,[PASSWORD] ,[ROLE] ,[EMAIL] ,CONTACT , REGISTRATIONDATE , LASTLOGINDATE) VALUES(@IdLIB , @Username, @Password, @Role , @Email , @Contact , @RegistrationDate , @LastLoginDate)";
                using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@IdLIB", user.IDLIB);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Role", user.Role);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Contact", user.Contact);
                    command.Parameters.AddWithValue("@RegistrationDate", user.RegistrationDate);
                    command.Parameters.AddWithValue("@LastLoginDate", user.LastLoginDate);

                    command.ExecuteNonQuery();
                }
            }
            return true;
        }
        public UserModel SignIn(string username, string password)
        {
            UserModel user = null;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string selectQuery = "SELECT * FROM LIBMANAGERS WHERE USERNAME = @Username AND PASSWORD = @Password";
                using (OleDbCommand command = new OleDbCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long iduser = Convert.ToInt64(reader["ID"]);
                            long idlib = Convert.ToInt64(reader["IDLIB"]);
                            string name = reader["USERNAME"].ToString();
                            string passwordUser = reader["PASSWORD"].ToString();
                            string role = reader["ROLE"].ToString();
                            string email = reader["EMAIL"].ToString();
                            string contact = reader["CONTACT"].ToString();
                            DateTime registrationDate = Convert.ToDateTime(reader["REGISTRATIONDATE"]);
                            DateTime lastLoginDate = Convert.ToDateTime(reader["LASTLOGINDATE"]);

                            user = new UserModel(iduser,idlib, name, passwordUser, role, email, contact, registrationDate, lastLoginDate); // Password is not returned for security reasons
                        }
                    }
                }
            }

            return user;
        }

        public List<UserModel> GetAllLibraryManagers()
        {
            List<UserModel> libraryManagers = new List<UserModel>();

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string selectQuery = "SELECT * FROM LIBMANAGERS";
                using (OleDbCommand command = new OleDbCommand(selectQuery, connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long iduser = Convert.ToInt64(reader["ID"]);
                            long idlib = Convert.ToInt64(reader["IDLIB"]);
                            string name = reader["USERNAME"].ToString();
                            string passwordUser = reader["PASSWORD"].ToString();
                            string role = reader["ROLE"].ToString();
                            string email = reader["EMAIL"].ToString();
                            string contact = reader["CONTACT"].ToString();
                            DateTime registrationDate = Convert.ToDateTime(reader["REGISTRATIONDATE"]);
                            DateTime lastLoginDate = Convert.ToDateTime(reader["LASTLOGINDATE"]);

                            UserModel libraryManager = new UserModel(iduser, idlib, name, passwordUser, role, email, contact, registrationDate, lastLoginDate);
                            libraryManagers.Add(libraryManager);
                        }
                    }
                }
            }

            return libraryManagers;
        }

        public UserModel GetLibraryManagerById(long id)
        {
            UserModel user = null;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string selectQuery = "SELECT * FROM LIBMANAGERS WHERE ID = @Id";
                using (OleDbCommand command = new OleDbCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long iduser = Convert.ToInt64(reader["ID"]);
                            long idlib = Convert.ToInt64(reader["IDLIB"]);
                            string name = reader["USERNAME"].ToString();
                            string passwordUser = reader["PASSWORD"].ToString();
                            string role = reader["ROLE"].ToString();
                            string email = reader["EMAIL"].ToString();
                            string contact = reader["CONTACT"].ToString();
                            DateTime registrationDate = Convert.ToDateTime(reader["REGISTRATIONDATE"]);
                            DateTime lastLoginDate = Convert.ToDateTime(reader["LASTLOGINDATE"]);

                            user = new UserModel(iduser, idlib, name, passwordUser, role, email, contact, registrationDate, lastLoginDate); 
                        }
                    }
                }
            }

            return user;
        }

        public bool DeleteLibraryManager(long id)
        {
            bool isDeleted = false;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string deleteQuery = "DELETE FROM LIBMANAGERS WHERE ID = @Id";
                using (OleDbCommand command = new OleDbCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsDeleted = command.ExecuteNonQuery();
                    isDeleted = rowsDeleted > 0; 
                }
            }

            return isDeleted;
        }


        public bool UpdateLibraryManager(long ID ,UserModel user)
        {
            bool isUpdated = false;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string updateQuery = "UPDATE LIBMANAGERS SET IDLIB = @IdLib, [USERNAME] = @Username, [PASSWORD] = @Password, [ROLE] = @Role, [EMAIL] = @Email, CONTACT = @Contact, REGISTRATIONDATE = @RegistrationDate , LASTLOGINDATE = @LastLoginDate WHERE ID = @Id";
                using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@IdLIB", user.IDLIB);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Role", user.Role);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Contact", user.Contact);
                    command.Parameters.AddWithValue("@RegistrationDate", user.RegistrationDate);
                    command.Parameters.AddWithValue("@LastLoginDate", user.LastLoginDate); // Update last login date on update
                    command.Parameters.AddWithValue("@Id", ID);
                    int rowsUpdated = command.ExecuteNonQuery();
                    isUpdated = rowsUpdated > 0; // Check if any rows were updated
                }
            }

            return isUpdated;
        }


        
    }
}
