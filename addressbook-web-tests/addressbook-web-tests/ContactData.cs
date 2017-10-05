using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    class ContactData
    {
        private string fname;
        private string mname = "";
        private string lname;

        public ContactData(string fname, string lname) // fname, lname - необходимые идентификаторы контакта
        {
            this.fname = fname;
            this.lname = lname;
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
