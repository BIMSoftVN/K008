using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace HelloWorld
{
    [TestFixture]
    public class ToanTu
    {
        [Test]
        public void Test()
        {
            int x = 5;

            Console.WriteLine(!(x <= 4));
        }
    }
}
