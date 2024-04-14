using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using BookStore.Utils;
using BookStore.Model.Borrow;

namespace BookStore.Controller
{
    class BorrowController
    {
        public bool AddBorrow(BorrowModel borrow)
        {
            bool isAdded = false;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string insertQuery = "INSERT INTO BORROW (IDMEMBER, BOOKID, BORROWDATE, RETURNDATE, STATUS) " +
                                     "VALUES (@IDMember, @BookID, @BorrowDate, @ReturnDate, @Status)";
                using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@IDMember", borrow.IDMember);
                    command.Parameters.AddWithValue("@BookID", borrow.BookID);
                    command.Parameters.AddWithValue("@BorrowDate", borrow.BorrowDate);
                    command.Parameters.AddWithValue("@ReturnDate", borrow.ReturnDate);
                    command.Parameters.AddWithValue("@Status", borrow.Status);

                    int rowsAffected = command.ExecuteNonQuery();
                    isAdded = rowsAffected == 1; // Successful addition if 1 row affected
                }
            }

            return isAdded;
        }

        public List<BorrowModel> GetAllBorrows()
        {
            List<BorrowModel> borrows = new List<BorrowModel>();

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string selectQuery = "SELECT * FROM BORROW";
                using (OleDbCommand command = new OleDbCommand(selectQuery, connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int borrowID = Convert.ToInt32(reader["BORROWID"]);
                            int idMember = Convert.ToInt32(reader["IDMEMBER"]);
                            int bookID = Convert.ToInt32(reader["BOOKID"]);
                            DateTime borrowDate = Convert.ToDateTime(reader["BORROWDATE"]);
                            DateTime returnDate = Convert.ToDateTime(reader["RETURNDATE"]);
                            string status = reader["STATUS"].ToString();

                            BorrowModel borrow = new BorrowModel(borrowID, idMember, bookID, borrowDate, returnDate, status);
                            borrows.Add(borrow);
                        }
                    }
                }
            }

            return borrows;
        }
    }
}
