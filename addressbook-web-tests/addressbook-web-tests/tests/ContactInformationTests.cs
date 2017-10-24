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
    class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformationTableVsEdit()
        {// данные в таблице соответствуют данным FromEditForm 
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromEditForm = app.Contacts.GetContactInformationFromEditForm(0);
            // verification
            Assert.AreEqual(fromTable, fromEditForm);
            Assert.AreEqual(fromTable.Address, fromEditForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromEditForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromEditForm.AllEmails);
        }

        [Test]
        public void TestContactInformationDetailVsEdit()
        {// данные в форме Details соответствуют данным FromEditForm 
            ContactData fromDetail = app.Contacts.GetContactInformationFromDetail(0);
            ContactData fromEditForm = app.Contacts.GetContactInformationFromEditForm(0);

            // verification
            Assert.AreEqual(fromDetail.Details, fromEditForm.Details);
        }
    }
}
