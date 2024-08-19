using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    [TestFixture]
    internal class CauLenhIf
    {
        [Test]
        public void Test()
        {
            int Tuoi = 14;

            if (Tuoi >=18)
            {
                Console.WriteLine("Bạn đủ tuổi");
            }
            else if (Tuoi>14)
            {
                Console.WriteLine("Bạn truy cập cần có người lớn");
            }    
            else
            {
                Console.WriteLine("Bạn không đủ tuổi");
            }
        }
    }
}
