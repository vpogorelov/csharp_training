using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WebAddressbookTests
{
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newContactData = new ContactData("newFirstname", "newLastname");
            newContactData.Mname = "newMName";

            app.Contacts.Modify(1, newContactData);
        }
    }
}
