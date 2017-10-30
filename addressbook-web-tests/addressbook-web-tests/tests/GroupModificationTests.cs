using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            List<GroupData> oldGroups = GroupData.GetAll();
            if (!app.Groups.AGroupExists())                    // если модифицировать нечего,
            {
                app.Groups.Create(new GroupData("tmpGroup"));   // ... то сначала создать
                oldGroups = GroupData.GetAll();
            }
            GroupData newData = new GroupData("modif201017");
            newData.Footer = "modifFFF"; //null;
            newData.Header = "modifHHH"; //null;

            GroupData toBeModification = oldGroups[0];

            app.Groups.Modify(toBeModification, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            foreach (GroupData group in newGroups)
            {
                if (group.Id == toBeModification.Id)
                    Assert.AreEqual(newData.Name, group.Name);
            }
        }
    }
}
