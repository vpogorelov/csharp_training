using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WebAddressbookTests
{
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            if (!app.Contacts.AContactExists())                             // если модифицировать нечего,
                app.Contacts.Create(new ContactData("tmpName", "tmpName")); // ... то сначала создать

            ContactData newContactData = new ContactData("modifFirstname", "modifLastname");
            newContactData.Mname = "modifMName";

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Modify(0, newContactData);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();

            oldContacts[0].Fname = newContactData.Fname;
            oldContacts[0].Lname = newContactData.Lname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
