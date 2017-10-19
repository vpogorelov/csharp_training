using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

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

        public ContactHelper Modify(int number, ContactData newContactData)
        {
            InitContactModification(number);
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

            Type(By.Name("firstname"), contact.Fname);
            Type(By.Name("middlename"), contact.Mname);
            Type(By.Name("lastname"), contact.Lname);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCash = null;
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

        public ContactHelper RemoveContact()
        {   // button [Delete].Click
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper ConfirmRemoveContact()
        {   // "Delete 1 addresses?" => [OK].Click
            driver.SwitchTo().Alert().Accept();
            contactCash = null;
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper ConfirmContactModification()
        {   // button [Update].Click
            driver.FindElement(By.Name("update")).Click();
            contactCash = null;
            return this;
        }

        public bool AContactExists()
        {
            manager.Navigator.GoToHomePage();
            return IsElementPresent(By.Name("selected[]"));
        }

        private List<ContactData> contactCash = null;

        public List<ContactData> GetContactList()
        {
            if (contactCash == null)
            {
                contactCash = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                int count = GetContactCount();
                for (int i = 1; i <= count; i++)
                {
                    IWebElement e2 = driver.FindElement(By.XPath("//tr[@name = 'entry'][" + i + "]/td[2]"));
                    IWebElement e3 = driver.FindElement(By.XPath("//tr[@name = 'entry'][" + i + "]/td[3]"));
                    contactCash.Add(new ContactData(e3.Text, e2.Text));
                }
            }
            return new List<ContactData>(contactCash);

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
    }
}
