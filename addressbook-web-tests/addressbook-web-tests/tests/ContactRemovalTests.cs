using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if(!app.Contacts.AContactExists())
                app.Contacts.Create(new ContactData("tmpName", "tmpName"));

            app.Contacts.Remove(1);
        }
    }
}
