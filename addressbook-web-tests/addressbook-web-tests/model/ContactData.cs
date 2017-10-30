using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name ="addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public ContactData()
        {
        }

        public ContactData(string firstName, string lastName) // firstName, lname - необходимые идентификаторы контакта
        {
            FirstName = firstName;
            LastName = lastName;
        }

        private string allPhones;
        private string allEmails;
        private string fullName;
        private string details;

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;
            if (Object.ReferenceEquals(this, other))
                return true;
            return ((FirstName == other.FirstName) && (LastName == other.LastName));
        }

        public override int GetHashCode()
        {
            return (FirstName + LastName).GetHashCode();
        }

        public override string ToString()
        {
            return "firstName_lastname=" + FirstName + "_" + LastName + "\nmiddlename = " + MiddleName;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
                return 1;
            return (FirstName + LastName).CompareTo(other.FirstName + other.LastName);
        }

        [Column(Name = "firstname")]
        public string FirstName { get; set; }
        [Column(Name = "middlename")]
        public string MiddleName { get; set; }
        [Column(Name = "nickname")]
        public string NickName { get; set; }
        [Column(Name = "lastname")]
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                if (fullName != null)
                    return fullName;
                else
                    return (FirstName + " " + (MiddleName == "" ? "" : MiddleName + " ") + LastName).Trim();
            }
            set
            {
                fullName = value;
            }
        }
        [Column(Name = "title")]
        public string Title { get; set; }
        [Column(Name = "company")]
        public string Company { get; set; }
        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }
        [Column(Name = "address")]
        public string Address { get; set; }
        [Column(Name = "address2")]
        public string SecondaryAddress { get; set; }
        [Column(Name = "home")]
        public string HomePhone { get; set; }
        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }
        [Column(Name = "work")]
        public string WorkPhone { get; set; }
        [Column(Name = "fax")]
        public string FaxPhone { get; set; }
        [Column(Name = "phone2")]
        public string SecondaryPhone { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                    return allPhones;
                else
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone) + CleanUp(SecondaryPhone)).Trim();
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
            else return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace(".", "") + "\r\n"; ;
            //else return Regex.Replace(phone, "[ -()]", "") + "\r\n";// ! не обрабатывает символы: '-', '.'
        }

        [Column(Name = "email")]
        public string Email1 { get; set; }
        [Column(Name = "email2")]
        public string Email2 { get; set; }
        [Column(Name = "email3")]
        public string Email3 { get; set; }
        [Column(Name = "homepage")]
        public string HomePage { get; set; }

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
        [Column(Name = "bday")]
        public string BirthDay { get; set; }
        [Column(Name = "bmonth")]
        public string BirthMonth { get; set; }
        [Column(Name = "byear")]
        public string BirthYear { get; set; }
        [Column(Name = "aday")]
        public string AnniversaryDay { get; set; }
        [Column(Name = "amonth")]
        public string AnniversaryMonth { get; set; }
        [Column(Name = "ayear")]
        public string AnniversaryYear { get; set; }
        [Column(Name = "notes")]
        public string Notes { get; set; }
        public string Details
        {
            get
            {
                if (details == null)
                {
                    details = ((FirstName == "" ? "" : FirstName + " ") + (MiddleName == "" ? "" : MiddleName + " ") + LastName).Trim();
                    ToDetailsString(NickName);
                    ToDetailsString(Title);
                    ToDetailsString(Company);
                    ToDetailsString(Address);
                    ToDetailsString(HomePhone, "H: ");
                    ToDetailsString(MobilePhone, "M: ");
                    ToDetailsString(WorkPhone, "W: ");
                    ToDetailsString(FaxPhone, "F: ");
                    ToDetailsString(Email1);
                    ToDetailsString(Email2);
                    ToDetailsString(Email3);
                    ToDetailsString(HomePage, "Homepage:\r\n");
                    ToDetailsString(DateToString(BirthDay, BirthMonth, BirthYear), "Birthday ");
                    ToDetailsString(DateToString(AnniversaryDay, AnniversaryMonth, AnniversaryYear), "Anniversary ");
                    ToDetailsString(SecondaryAddress);
                    ToDetailsString(SecondaryPhone, "P: ");
                    ToDetailsString(Notes);
                    details = details.Replace("    ", " ").Replace("   ", " ").Replace("  ", " ");// доделать
                }
                return details;
            }
            set
            {
                details = value.Replace("\n\r", "").Trim(); // "\r\n\r\n"-->"\r\n"
            }
        }

        private string DateToString(string day, string month, string year)
        {
            string dateStr = (day == "0" ? "" : day + ". ") + (month == "-" ? "" : FirstSymbToCaps(month) + " ") + year;
            if (year != null && year != "")
            {
                Match m = new Regex(@"\d+").Match(year);
                if (m.Length == 4 && Int32.Parse(m.Value) > 1867 && Int32.Parse(m.Value) < 2018)//при == 1867 - зависимости от Day & Month, нужно ТЗ
                    dateStr += " (" + (2017 - Int32.Parse(m.Value)) + ")";
            }
            return dateStr.Trim();
        }

        private string FirstSymbToCaps(string monthString)
        {
            if (monthString == null || monthString == "")
                return monthString;
            else return monthString.Remove(1).ToUpper()+ monthString.Substring(1);
        }

        private void ToDetailsString(string addString, string prefixString = "")
        {
            if (addString != null && addString != "")
                details += "\r\n" + prefixString + addString;
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
               return (from g in db.Contacts select g).ToList();
            }

        }
    }
}
