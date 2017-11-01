using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
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
            List<ContactData> contacts = ContactData.GetAll();
            int groupIndex = 0;
            if (contacts.Count > 0)
            {
                while ((groupIndex < groups.Count) && (groups[groupIndex].GetContacts().Count >= contacts.Count))   // если в этой группе есть все существующие контакты
                    groupIndex++;
            }
            if ((contacts.Count < 1) || (groupIndex >= groups.Count))  // если в каждой группе уже есть все существующие контакты
            {
                ContactData tmpContact = new ContactData("F.Name_4_add_to_group", "L.Name_4_add_to_group")
                {
                    MiddleName = "M.Name_4_add_to_group"
                };
                app.Contacts.Create(tmpContact);
                contacts = ContactData.GetAll();
                groupIndex = 0;
            }

            GroupData group = groups[groupIndex];
            List<ContactData> oldList = group.GetContacts();

            //ContactData contact = contacts.Except(oldList).First();
            //
            //  Если для контакта в contacts[] существует "тёзка" в oldList[], отличающийся только Id,
            //то contacts.Except(oldList) удаляет из contacts обоих.
            //Отсюда легко моделируется ситуация, когда contacts.Except(oldList) возвращает ошибочно пустой IEnumerable,
            //и .First() от него ==> System.InvalidOperationException: 'Последовательность не содержит элементов'
            // Ищем в contacts[] contact, отсутствующий в oldList[] vv
            ContactData contact = null;
            int contactIndex = 0;
            do {
                int contactInGroupIndex = 0;
                while ((contactInGroupIndex < oldList.Count())
                    && (contacts[contactIndex].Id != oldList[contactInGroupIndex].Id))
                    contactInGroupIndex++;
                if (contactInGroupIndex < oldList.Count())  //contacts[contactIndex] обнаружен в oldList[]
                    contactIndex++;
                else
                    contact = contacts[contactIndex];
            } while (contact == null);

            //    нашли     ^^
            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
