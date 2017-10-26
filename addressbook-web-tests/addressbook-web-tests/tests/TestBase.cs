using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager app;

        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }

        public static Random rnd = new Random();

        public static string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 1; i < l; i++)
            {
                int rI = 32 + Convert.ToInt32(rnd.NextDouble() * 65);
                if (rI == 39)// ' - убираем
                    rI = 40;
                builder.Append(Convert.ToChar(rI));
            }
            return builder.ToString();
        }
    }
}
