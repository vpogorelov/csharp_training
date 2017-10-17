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
    public class NavigationHelper : HelperBase
    {
        protected string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL)
            : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void GoToHomePage()
        {
            //if (!(driver.Url == baseURL + "addressbook/")) // cancel 'if()': находясь на HomePage имеет смысл обновлять страницу, её содержание могло измениться
            driver.Navigate().GoToUrl(baseURL + "addressbook/");
        }

        public void GoToGroupsPage()
        {
            //if (!(driver.Url == baseURL + "addressbook/group.php"
            //    && IsElementPresent(By.Name("new"))))// cancel 'if()': находясь на GroupsPage имеет смысл обновлять страницу, её содержание могло измениться
            driver.FindElement(By.LinkText("groups")).Click();
        }
    }
}
