using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using K008Libs;
using K008Libs.MaHoa;

namespace iTask.Test
{
    [TestFixture]
    public class clTest
    {
        [Test]
        public void Test()
        { 
            string Connstring = "Server=./;Database=iTask;TrustServerCertificate=True;MultipleActiveResultSets=True;";
            
            string MaHoa = EncryptionbyCode.Encrypt(Connstring, "123456");
            Console.WriteLine(Connstring);
            Console.WriteLine(MaHoa);

            string GiaiMa = EncryptionbyCode.Decrypt(MaHoa, "123456");
            Console.WriteLine(GiaiMa);
        }
    }
}
