using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WebAddressbookTests
{
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            if (!app.Contacts.AContactExists())
                app.Contacts.Create(new ContactData("tmpName", "tmpName"));

            ContactData newContactData = new ContactData("newFirstname", "newLastname");
            //newContactData.Mname = "newMName";

            app.Contacts.Modify(0, newContactData);
        }
    }
}
