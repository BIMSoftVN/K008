using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    [TestFixture]
    internal class VongLapFor
    {
        [Test]
        public void ForLongNhau()
        {
            for (int i = 2; i <= 9; i+=2) 
            {
                for (int j = 1; j <= 9; j++)
                {
                    Console.WriteLine($"{i} x {j} = {i*j}");
                }    
            };
        }

        [Test]
        public void Foreach()
        {
            string[] Colors = { "Xanh", "Đỏ", "Tím", "Vàng" };
            foreach (string c in Colors)
            {
                Console.WriteLine(c);
            }    

            //for (int i = 0; i < Colors.Length; i++) 
            //{
            //    Console.WriteLine(Colors[i]);
            //}
        }

        [Test]
        public void BreakFor()
        {
            for (int i = 1; i <= 9; i++)
            {
                //if (i==4)
                //{
                //    break;
                //}    

                if (i>=4 && i<=6)
                {
                    continue;
                }    

                Console.WriteLine(i);
            };
        }
    }
}
