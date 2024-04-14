using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStore.Model.Library
{
    class LibraryModel
    {
        public long IDLIB { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public LibraryModel(long idlib, string name, string location, string email, string phone)
        {
            IDLIB = idlib;
            Name = name;
            Location = location;
            Email = email;
            Phone = phone;
        }

        public LibraryModel(string name, string location, string email, string phone)
        {
            Name = name;
            Location = location;
            Email = email;
            Phone = phone;
        }

        // You can also keep the parameterless constructor if needed
        public LibraryModel() { }
    }
}
