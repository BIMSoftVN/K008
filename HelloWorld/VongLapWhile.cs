using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    [TestFixture]
    internal class VongLapWhile
    {
        [Test]
        public void While()
        {
            int i = 11;

            Console.WriteLine("While");
            while (i <= 10)
            {
                Console.WriteLine(i);
                i += 2;
            }

            i = 11;

            Console.WriteLine("Do-While");
            do
            {
                Console.WriteLine(i);
                i += 2;
            }
            while (i <= 10);
        }

        [Test]
        public void BreakWhile()
        {
            int i = 1;

            Console.WriteLine("While");
            while (i <= 10)
            {
                if (i==5)
                {
                    i++;
                    continue;
                }    
                Console.WriteLine(i);
                i++;
            }
        }
    }
}
