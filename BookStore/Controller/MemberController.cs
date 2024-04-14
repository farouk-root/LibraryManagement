using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.Model.Member;
using BookStore.Utils;
using System.Data.OleDb;

namespace BookStore.Controller
{
    class MemberController
    {
        public bool AddMember(MemberModel member)
        {
            bool isAdded = false;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string insertQuery = "INSERT INTO MEMBER (IDLIB, NAME, CONTACTPHONE, STATUS, EMAIL, HOMEADDRESS, MEMBERSHIPSTARTDATE, MEMBERSHIPEXPIRATION) " +
                                     "VALUES (@IdLib,@Name, @ContactPhone, @Status, @Email, @HomeAddress, @MembershipStartDate, @MembershipExpiration)";
                using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@IdLib", member.IDlib);
                    command.Parameters.AddWithValue("@Name", member.Name);
                    command.Parameters.AddWithValue("@ContactPhone", member.ContactPhone);
                    command.Parameters.AddWithValue("@Status", member.Status);
                    command.Parameters.AddWithValue("@Email", member.Email);
                    command.Parameters.AddWithValue("@HomeAddress", member.HomeAddress);
                    command.Parameters.AddWithValue("@MembershipStartDate", member.MembershipStartDate);
                    command.Parameters.AddWithValue("@MembershipExpiration", member.MembershipExpiration);

                    int rowsAffected = command.ExecuteNonQuery();
                    isAdded = rowsAffected == 1; // Successful addition if 1 row affected
                }
            }

            return isAdded;
        }

        public bool UpdateMember(MemberModel member , long IDMember)
        {
            bool isUpdated = false;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string updateQuery = "UPDATE MEMBER SET IDLIB = @IdLib, NAME = @Name, CONTACTPHONE = @ContactPhone, STATUS = @Status, EMAIL = @Email, HOMEADDRESS = @HomeAddress, MEMBERSHIPSTARTDATE = @MembershipStartDate, MEMBERSHIPEXPIRATION = @MembershipExpiration WHERE IDMEMBER = @MemberID";
                using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@IdLib", member.IDlib);
                    command.Parameters.AddWithValue("@Name", member.Name);
                    command.Parameters.AddWithValue("@ContactPhone", member.ContactPhone);
                    command.Parameters.AddWithValue("@Status", member.Status);
                    command.Parameters.AddWithValue("@Email", member.Email);
                    command.Parameters.AddWithValue("@HomeAddress", member.HomeAddress);
                    command.Parameters.AddWithValue("@MembershipStartDate", member.MembershipStartDate);
                    command.Parameters.AddWithValue("@MembershipExpiration", member.MembershipExpiration);
                    command.Parameters.AddWithValue("@MemberID", IDMember);

                    int rowsAffected = command.ExecuteNonQuery();
                    isUpdated = rowsAffected > 0; // Successful update if at least one row affected
                }
            }

            return isUpdated;
        }


        public bool DeleteMember(long memberID)
        {
            bool isDeleted = false;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string deleteQuery = "DELETE FROM MEMBER WHERE IDMEMBER = @MemberID";
                using (OleDbCommand command = new OleDbCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@MemberID", memberID);

                    int rowsAffected = command.ExecuteNonQuery();
                    isDeleted = rowsAffected > 0; // Successful deletion if at least one row affected
                }
            }

            return isDeleted;
        }


        public List<MemberModel> GetAllMembersByLibraryID(long libraryID)
        {
            List<MemberModel> members = new List<MemberModel>();

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string selectQuery = "SELECT * FROM MEMBER WHERE IDLIB = @LibraryID";
                using (OleDbCommand command = new OleDbCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@LibraryID", libraryID);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idMember = Convert.ToInt32(reader["IDMEMBER"]);
                            string name = reader["NAME"].ToString();
                            string contactPhone = reader["CONTACTPHONE"].ToString();
                            string status = reader["STATUS"].ToString();
                            string email = reader["EMAIL"].ToString();
                            string homeAddress = reader["HOMEADDRESS"].ToString();
                            DateTime membershipStartDate = Convert.ToDateTime(reader["MEMBERSHIPSTARTDATE"]);
                            DateTime membershipExpiration = Convert.ToDateTime(reader["MEMBERSHIPEXPIRATION"]);

                            MemberModel member = new MemberModel(idMember, libraryID, name, contactPhone, status, email, homeAddress, membershipStartDate, membershipExpiration);
                            members.Add(member);
                        }
                    }
                }
            }

            return members;
        }

        public MemberModel GetMemberByID(long memberID)
        {
            MemberModel member = null;

            using (OleDbConnection connection = connectionDB.GetConnection())
            {
                connection.Open();
                string selectQuery = "SELECT * FROM MEMBER WHERE IDMEMBER = @MemberID";
                using (OleDbCommand command = new OleDbCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@MemberID", memberID);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long idLibrary = Convert.ToInt64(reader["IDLIB"]);
                            string name = reader["NAME"].ToString();
                            string contactPhone = reader["CONTACTPHONE"].ToString();
                            string status = reader["STATUS"].ToString();
                            string email = reader["EMAIL"].ToString();
                            string homeAddress = reader["HOMEADDRESS"].ToString();
                            DateTime membershipStartDate = Convert.ToDateTime(reader["MEMBERSHIPSTARTDATE"]);
                            DateTime membershipExpiration = Convert.ToDateTime(reader["MEMBERSHIPEXPIRATION"]);

                            member = new MemberModel(memberID, idLibrary, name, contactPhone, status, email, homeAddress, membershipStartDate, membershipExpiration);
                        }
                    }
                }
            }

            return member;
        }

    }
}
