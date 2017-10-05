using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace addressbook_web_tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodSquare()
        {
            Square s1 = new Square(1);
            Square s2 = new Square(0);
            Square s3 = s1;

            Assert.AreEqual(s1.Size, 1);
            Assert.AreEqual(s2.Size, 0);
            Assert.AreEqual(s3.Size, 1);

            s3.Size = 15;
            Assert.AreEqual(s1.Size, 15);

            s3.Colored = false;
            Assert.AreNotEqual(s1.Colored, true);
        }

        [TestMethod]
        public void TestMethodCircle()
        {
            Circle s1 = new Circle(1);
            Circle s2 = new Circle(0);
            Circle s3 = s1;

            Assert.AreEqual(s1.Radius, 1);
            Assert.AreEqual(s2.Radius, 0);
            Assert.AreEqual(s3.Radius, 1);

            s3.Radius = 15;
            Assert.AreEqual(s1.Radius, 15);

            s1.Colored = true;
            Assert.AreNotEqual(s3.Colored, false);
        }
    }
}
