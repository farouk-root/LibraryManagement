using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStore.Model.Member
{
    public class MemberModel
    {
        public long IDMember { get; set; } // Auto-incremented primary key
        public long IDlib { get; set; }
        public string Name { get; set; }
        public string ContactPhone { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string HomeAddress { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipExpiration { get; set; }

        public MemberModel(long idMember,long idlib ,string name, string contactPhone, string status, string email, string homeAddress, DateTime membershipStartDate, DateTime membershipExpiration)
        {
            IDMember = idMember;
            Name = name;
            ContactPhone = contactPhone;
            Status = status;
            Email = email;
            HomeAddress = homeAddress;
            MembershipStartDate = membershipStartDate;
            MembershipExpiration = membershipExpiration;
        }

        public MemberModel( long idLib , string name, string contactPhone, string status, string email, string homeAddress, DateTime membershipStartDate, DateTime membershipExpiration)
        {
            this.IDlib = idLib;
            Name = name;
            ContactPhone = contactPhone;
            Status = status;
            Email = email;
            HomeAddress = homeAddress;
            MembershipStartDate = membershipStartDate;
            MembershipExpiration = membershipExpiration;
        }


        // Empty constructor
        public MemberModel()
        {
            // Default values for properties can be assigned here if needed
        }
    }

}
