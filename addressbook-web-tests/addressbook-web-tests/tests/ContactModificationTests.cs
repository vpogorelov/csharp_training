using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            List<ContactData> oldContacts = ContactData.GetAll();

            if (!app.Contacts.AContactExists())                             // если модифицировать нечего,
            {
                app.Contacts.Create(new ContactData("tmpName", "tmpName")); // ... то сначала создать
                oldContacts = ContactData.GetAll();
            }
            ContactData newContactData = new ContactData("modif2010Firstname", "modifLastname");
            newContactData.MiddleName = "modifMName";

            ContactData toBeModification = oldContacts[0];

            app.Contacts.Modify(toBeModification, newContactData);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[0].FirstName = newContactData.FirstName;
            oldContacts[0].LastName = newContactData.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            foreach(ContactData contact in newContacts)
            {
                if (contact.Id == toBeModification.Id)
                    Assert.AreEqual(newContactData.CompareTo(contact), 0);
            }
        }
    }
}
