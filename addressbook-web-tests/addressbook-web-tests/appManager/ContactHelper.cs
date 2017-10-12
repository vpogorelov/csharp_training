using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return this;
        }

        public ContactHelper Modify(int number, ContactData newContactData)
        {
            InitContactModification(number);
            FillContactForm(newContactData);
            ConfirmContactModification();
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
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {   // checkBox.Select
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {   // buttom [Delete].Click
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper ConfirmRemoveContact()
        {   // "Delete 1 addresses?" => [OK].Click
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper InitContactModification(int number)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + number + "]")).Click();
            return this;
        }

        public ContactHelper ConfirmContactModification()
        {   // button [Update].Click
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public bool AContactExists()
        {
            manager.Navigator.GoToHomePage();
            return IsElementPresent(By.Name("selected[]"));
        }
    }
}
