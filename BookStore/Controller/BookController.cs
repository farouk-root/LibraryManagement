using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.Model.Book;
using System.Data.OleDb;
using BookStore.Utils;

namespace BookStore.Controller
{
    class BookController
    {

        public List<BookModel> GetAllBooks()
        {
            List<BookModel> books = new List<BookModel>();

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string selectQuery = "SELECT * FROM BOOK";
                using (OleDbCommand command = new OleDbCommand(selectQuery, connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long bookID = Convert.ToInt64(reader["BOOKID"]);
                            long idLibrary = Convert.ToInt64(reader["IDLIB"]);
                            string title = reader["TITLE"].ToString();
                            string author = reader["AUTHOR"].ToString();
                            string genre = reader["GENRE"].ToString();
                            string description = reader["DESCRIPTION"].ToString();
                            int nbCopies = Convert.ToInt32(reader["NBCOPIES"]);
                            int nbCopiesAvailable = Convert.ToInt32(reader["NBCOPIESAVAILABLE"]);

                            BookModel book = new BookModel(bookID, idLibrary, title, author, genre, description, nbCopies, nbCopiesAvailable);
                            books.Add(book);
                        }
                    }
                }
            }

            return books;
        }

        public bool AddBook(BookModel book)
        {
            bool isAdded = false;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string insertQuery = "INSERT INTO BOOK (IDLIB, TITLE, AUTHOR, GENRE, DESCRIPTION, NBCOPIES, NBCOPIESAVAILABLE) VALUES (@IDLib, @Title, @Author, @Genre, @Description, @NbCopies, @NbCopiesAvailable)";
                using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@IDLib", book.IDLibrary);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@Genre", book.Genre);
                    command.Parameters.AddWithValue("@Description", book.Description);
                    command.Parameters.AddWithValue("@NbCopies", book.NbCopies);
                    command.Parameters.AddWithValue("@NbCopiesAvailable", book.NbCopiesAvailable);

                    int rowsAffected = command.ExecuteNonQuery();
                    isAdded = rowsAffected == 1; // Successful addition if 1 row affected
                }
            }

            return isAdded;
        }

        public BookModel GetBookByIdAndLibraryId(long bookId, long libraryId)
        {
            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string selectQuery = "SELECT * FROM BOOK WHERE BOOKID = @BookID AND IDLIB = @LibraryID";
                using (OleDbCommand command = new OleDbCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@BookID", bookId);
                    command.Parameters.AddWithValue("@LibraryID", libraryId);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long fetchedBookID = Convert.ToInt64(reader["BOOKID"]);
                            long fetchedLibraryID = Convert.ToInt64(reader["IDLIB"]);
                            string title = reader["TITLE"].ToString();
                            string author = reader["AUTHOR"].ToString();
                            string genre = reader["GENRE"].ToString();
                            string description = reader["DESCRIPTION"].ToString();
                            int nbCopies = Convert.ToInt32(reader["NBCOPIES"]);
                            int nbCopiesAvailable = Convert.ToInt32(reader["NBCOPIESAVAILABLE"]);

                            return new BookModel(fetchedBookID, fetchedLibraryID, title, author, genre, description, nbCopies, nbCopiesAvailable);
                        }
                    }
                }
            }

            // If no book is found with the given IDs, return null or handle the situation accordingly.
            return null;
        }

        public bool UpdateBook(BookModel book , long idBook)
        {
            bool isUpdated = false;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string updateQuery = "UPDATE BOOK SET TITLE = @Title, AUTHOR = @Author, GENRE = @Genre, DESCRIPTION = @Description, NBCOPIES = @NbCopies, NBCOPIESAVAILABLE = @NbCopiesAvailable WHERE BOOKID = @BookID AND IDLIB = @IDLib";
                using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@Genre", book.Genre);
                    command.Parameters.AddWithValue("@Description", book.Description);
                    command.Parameters.AddWithValue("@NbCopies", book.NbCopies);
                    command.Parameters.AddWithValue("@NbCopiesAvailable", book.NbCopiesAvailable);
                    command.Parameters.AddWithValue("@BookID", idBook);
                    command.Parameters.AddWithValue("@IDLib", book.IDLibrary);

                    int rowsAffected = command.ExecuteNonQuery();
                    isUpdated = rowsAffected > 0; // Successful update if at least one row affected
                }
            }

            return isUpdated;
        }

        public bool DeleteBook(long bookID)
        {
            bool isDeleted = false;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string deleteQuery = "DELETE FROM BOOK WHERE BOOKID = @BookID";
                using (OleDbCommand command = new OleDbCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@BookID", bookID);

                    int rowsAffected = command.ExecuteNonQuery();
                    isDeleted = rowsAffected > 0; // Successful deletion if at least one row affected
                }
            }

            return isDeleted;
        }




    }
}
