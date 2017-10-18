using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string fname;
        private string mname = "";
        private string lname;

        public ContactData(string fname, string lname) // fname, lname - необходимые идентификаторы контакта
        {
            this.fname = fname;
            this.lname = lname;
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

        public string Fname
        {
            get
            {
                return fname;
            }
            set
            {
                fname = value;
            }
        }
        public string Mname
        {
            get
            {
                return mname;
            }
            set
            {
                mname = value;
            }
        }
        public string Lname
        {
            get
            {
                return lname;
            }
            set
            {
                lname = value;
            }
        }
    }
}
