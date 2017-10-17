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
            if (!app.Groups.AGroupExists())
                app.Groups.Create(new GroupData("tmpGroup"));

            GroupData newData = new GroupData("new121017");
            newData.Footer = "newFFF"; //null;// "newFFF";
            newData.Header = "newHHH"; //null;// "newHHH";

            app.Groups.Modify(0, newData);
        }
    }
}
