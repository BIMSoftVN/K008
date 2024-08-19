using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    public class clConNguoi
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class clKySu : clConNguoi
    {
        public string ChucVu { get; set; }
    }


    public class TestClass
    {
        public void Test()
        {
            clKySu ksTruc = new clKySu();
        }
    }
}
