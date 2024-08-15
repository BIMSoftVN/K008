using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    [TestFixture]
    public class HelloWorld
    {
        [Test]
        public void Test()
        {
            //Dòng code này dùng để ghi ra màn hình
            
            // Char -> int -> long -> float -> double Tự động chuyển

            int Age = 18;
            double Age2 = 12;
            //Age = (int) Age2;

            Age = Convert.ToInt32(Age2);

            Console.WriteLine("Giá trị của Age : " + Age);
        }
    }
}
