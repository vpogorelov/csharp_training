using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    class RemoveContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void TestRemoveContactFromGroup()
        {
            List<GroupData> groups = GroupData.GetAll();
            if (groups.Count < 1)
            {
                app.Groups.Create(new GroupData("newGroup")
                {
                    Header = "hhh",
                    Footer = "fff"
                });
                groups = GroupData.GetAll();
            }
            if (ContactData.GetAll().Count < 1)
            {
                app.Contacts.Create(new ContactData("F.Name", "L.Name")
                {
                    MiddleName = "M.Name"
                });
            }
            int groupIndex = 0;
            while ((groupIndex < groups.Count) && (groups[groupIndex].GetContacts().Count < 1))
                groupIndex++;
            if (groupIndex >= groups.Count)  // все группы пустые
            {   // Prepair
                app.Contacts.AddContactToGroup(ContactData.GetAll()[0], groups[0]); // добавим контакт в группу (подготовка данных для тестирования)
                groupIndex = 0;
                while (groups[groupIndex].GetContacts().Count < 1)
                    groupIndex++;
            }

            GroupData group = GroupData.GetAll()[groupIndex];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = oldList[0];

            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.RemoveAt(0);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
