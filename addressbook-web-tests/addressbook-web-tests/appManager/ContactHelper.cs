using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager) { }

        public ContactHelper Remove(int number)
        {
            SelectContact(number);
            RemoveContact();
            ConfirmRemoveContact();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(ContactData group)
        {
            SelectContact(group.Id);
            RemoveContact();
            ConfirmRemoveContact();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(int number, ContactData newContactData)
        {
            InitContactModification(number);
            FillContactForm(newContactData);
            ConfirmContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(ContactData oldContactData, ContactData newContactData)
        {
            InitContactModification(oldContactData.Id);
            FillContactForm(newContactData);
            ConfirmContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Create(ContactData contact)
        {
            //manager.Navigator.GoToHomePage();
            InitNewContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {

            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("middlename"), contact.MiddleName);
            Type(By.Name("lastname"), contact.LastName);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {   // checkBox.Select
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper SelectContact(string id)
        {       // checkBox.Select
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @id='" + id + "'])")).Click();
            // +!: driver.FindElement(By.Id(id));
            return this;
        }

        public ContactHelper RemoveContact()
        {   // button [Delete].Click
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper ConfirmRemoveContact()
        {   // "Delete 1 addresses?" => [OK].Click
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            //driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[7].FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper InitContactModification(string id)
        {
            driver.FindElement(By.XPath("//input[@id='" + id + "']/../..//img[@alt='Edit']")).Click();
            return this;
        }

        public ContactHelper ConfirmContactModification()
        {   // button [Update].Click
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactDetails(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Details'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public bool AContactExists()
        {
            manager.Navigator.GoToHomePage();
            return IsElementPresent(By.Name("selected[]"));
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                int count = GetContactCount();
                for (int i = 1; i <= count; i++)
                {
                    IWebElement e2 = driver.FindElement(By.XPath("//tr[@name = 'entry'][" + i + "]/td[2]"));
                    IWebElement e3 = driver.FindElement(By.XPath("//tr[@name = 'entry'][" + i + "]/td[3]"));
                    ContactData contact = new ContactData(e3.Text, e2.Text);
                    contact.Id = driver.FindElement(By.XPath("//tr[@name = 'entry'][" + i + "]")).FindElement(By.TagName("input")).GetAttribute("Id");
                    contactCache.Add(contact);
                }
            }
            return new List<ContactData>(contactCache);

            //ICollection<IWebElement> lastNameColumn = driver.FindElements(By.XPath("//tr[@name = 'entry']//td[2]"));
            //ICollection<IWebElement> firstNameColumn = driver.FindElements(By.XPath("//tr[@name = 'entry']//td[3]"));

            //string xpatnwithIndex = "//tr[@name = 'entry']//td[N]";
            //ICollection<IWebElement> headersr = driver.FindElements(By.XPath("//tr/th[contains(@class, 'sortable fd-column')]"));
            //int countOfColumns = headersr.Count;
            //ICollection<IWebElement> lastNameColumn = driver.FindElements(By.XPath(xpatnwithIndex.Replace("N", "3")));
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.XPath("//tr[@name = 'entry']")).Count;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhones
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string nickName = driver.FindElement(By.Name("nickname")).GetAttribute("value");

            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");

            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string secondaryAddress = driver.FindElement(By.Name("address2")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string faxPhone = driver.FindElement(By.Name("fax")).GetAttribute("value");
            string secondaryPhone = driver.FindElement(By.Name("phone2")).GetAttribute("value");

            string email1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string homePage = driver.FindElement(By.Name("homepage")).GetAttribute("value");

            string birthDay = driver.FindElement(By.Name("bday")).GetAttribute("value");
            string birthMonth = driver.FindElement(By.Name("bmonth")).GetAttribute("value");
            string birthYear = driver.FindElement(By.Name("byear")).GetAttribute("value");
            string anniversaryDay = driver.FindElement(By.Name("aday")).GetAttribute("value");
            string anniversaryMonth = driver.FindElement(By.Name("amonth")).GetAttribute("value");
            string anniversaryYear = driver.FindElement(By.Name("ayear")).GetAttribute("value");

            string notes = driver.FindElement(By.Name("notes")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                MiddleName = middleName,
                NickName = nickName,
                Address = address,
                SecondaryAddress = secondaryAddress,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                FaxPhone = faxPhone,
                SecondaryPhone = secondaryPhone,
                Email1 = email1,
                Email2 = email2,
                Email3 = email3,
                HomePage = homePage,
                Title = title,
                Company = company,
                BirthDay = birthDay,
                BirthMonth = birthMonth,
                BirthYear = birthYear,
                AnniversaryDay = anniversaryDay,
                AnniversaryMonth = anniversaryMonth,
                AnniversaryYear = anniversaryYear,
                Notes = notes
            };
        }
        public ContactData GetContactInformationFromDetail(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactDetails(index);
            string details = driver.FindElement(By.Id("content")).Text;
            string CleanDetails = details.Replace("\n\r", ""); // "\r\n\r\n"-->"\r\n" 
            //string homePage = driver.FindElement(By.Id("content")).FindElement(By.TagName("a")).Text;
            return new ContactData("", "")
            {
                Details = details
            };
        }

        public int GetNumberOfSearchResult()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }

        internal void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            CleareGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
        }

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        private void CleareGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText("[all]");
        }
    }
}
