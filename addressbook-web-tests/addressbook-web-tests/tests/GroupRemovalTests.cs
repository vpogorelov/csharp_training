using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            List<GroupData> oldGroups = GroupData.GetAll();
            if (oldGroups.Count < 1)                           // если удалять нечего,
            {                    
                app.Groups.Create(new GroupData("tmpGroup"));   // сначала создать
                oldGroups = GroupData.GetAll();
            }
            GroupData toBeRemoved = oldGroups[0];

            app.Groups.Remove(toBeRemoved);   // удалить группу с индексом '0'

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll(); ;

            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);
            foreach(GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
