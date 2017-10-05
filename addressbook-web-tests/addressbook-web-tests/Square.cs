using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    class Square : Figure
    {
        private int side;

        public Square(int size)
        {
            this.side = size;
        }

        public int Size
        {
            get
            {
                return side;
            }
            set
            {
                side = value;
            }
        }
     }
}
