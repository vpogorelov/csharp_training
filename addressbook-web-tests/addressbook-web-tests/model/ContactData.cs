using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;

        public ContactData(string fname, string lname) // fname, lname - необходимые идентификаторы контакта
        {
            Fname = fname;
            Lname = lname;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;
            if (Object.ReferenceEquals(this, other))
                return true;
            return ((Fname == other.Fname) && (Lname == other.Lname));
        }

        public override int GetHashCode()
        {
            return (Fname + Lname).GetHashCode();
        }

        public override string ToString()
        {
            return "fname_lname=" + Fname + "_" + Lname;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
                return 1;
            return (Fname + Lname).CompareTo(other.Fname + other.Lname);
        }

        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                    return allPhones;
                else
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
            }
            set
            {
                allPhones = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
                return "";
            else return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
        }

        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                    return allEmails;
                else
                    return (Email1 + "\r\n" + (Email2 == "" ? "" : Email2 + "\r\n") + Email3).Trim();
            }
            set
            {
                allEmails = value;
            }
        }
    }
}
