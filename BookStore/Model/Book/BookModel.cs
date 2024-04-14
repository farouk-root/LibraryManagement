using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStore.Model.Book
{
    public class BookModel
    {
        public long BookID { get; set; }
        public long IDLibrary { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int NbCopies { get; set; }
        public int NbCopiesAvailable { get; set; }

        // Optional constructor (assuming properties have default values)
        public BookModel() { }

        // Constructor with all properties (useful for creating new BookModel instances)
        public BookModel(long bookID, long idLibrary, string title, string author,
                         string genre, string description, int nbCopies, int nbCopiesAvailable)
        {
            BookID = bookID;
            IDLibrary = idLibrary;
            Title = title;
            Author = author;
            Genre = genre;
            Description = description;
            NbCopies = nbCopies;
            NbCopiesAvailable = nbCopiesAvailable;
        }

        public BookModel(long idLibrary, string title, string author,
                         string genre, string description, int nbCopies, int nbCopiesAvailable)
        {
            IDLibrary = idLibrary;
            Title = title;
            Author = author;
            Genre = genre;
            Description = description;
            NbCopies = nbCopies;
            NbCopiesAvailable = nbCopiesAvailable;
        }
    }

}
