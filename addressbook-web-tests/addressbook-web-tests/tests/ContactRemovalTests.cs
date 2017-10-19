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
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            if(!app.Contacts.AContactExists())                                  // если удалять нечего,
                app.Contacts.Create(new ContactData("tmpName", "tmpName"));     // сначала создать

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Remove(0); // удалить контакт с индексом '0' (верхний)

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
