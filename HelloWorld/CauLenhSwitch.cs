using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    [TestFixture]
    internal class CauLenhSwitch
    {
        [Test]
        public void Test()
        {
            string Day = "Thu 4";

            switch (Day)
            {
                case "Thu 2":
                    Console.WriteLine("Bắt đầu tuần mới");
                    break;

                case "Thu 6":
                    Console.WriteLine("Sắp cuối tuần rồi");
                    break;

                case "Thu 7":
                case "Chu Nhat":
                    Console.WriteLine("Cuối tuần");
                    break;

                default:
                    Console.WriteLine("Một ngày bình thường");
                    break;
            }
        }
    }
}
