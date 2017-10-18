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
            if (!app.Groups.AGroupExists())                     // если модифицировать нечего,
                app.Groups.Create(new GroupData("tmpGroup"));   // ... то создаём

            GroupData newData = new GroupData("new121017");
            newData.Footer = "newFFF"; //null;// "newFFF";
            newData.Header = "newHHH"; //null;// "newHHH";

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Modify(0, newData);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
