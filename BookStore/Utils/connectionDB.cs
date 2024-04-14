using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace BookStore.Utils
{
    class connectionDB
    {
        public static OleDbConnection GetConnection()
        {
            const string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\farouk\\Desktop\\Maissa\\BookStore\\BookStore\\Maissav2.accdb;Persist Security Info=False;";
            return new OleDbConnection(connectionString);
        }
    }
}
