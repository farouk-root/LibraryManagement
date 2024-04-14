using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStore.Model.Borrow
{
    public class BorrowModel
    {
        public int BorrowID { get; set; } // Auto-incremented primary key
        public int IDMember { get; set; }
        public int BookID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; }

        public BorrowModel(int BorrowID , int idMember, int bookID, DateTime borrowDate, DateTime returnDate, string status)
        {
            this.BorrowID = BorrowID;
            IDMember = idMember;
            BookID = bookID;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            Status = status;
        }

        // Parameterized constructor
        public BorrowModel(int idMember, int bookID, DateTime borrowDate, DateTime returnDate, string status)
        {
            IDMember = idMember;
            BookID = bookID;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            Status = status;
        }


        // Empty constructor
        public BorrowModel()
        {
            // Default values for properties can be assigned here if needed
        }
    }
}
