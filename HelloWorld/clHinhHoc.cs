using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TaoHinh()
        { 
            var hv = new clHinhVuong();
            hv.Ten = "hình vuông";
            hv.Canh = 10;
            hv.TinhDienTich();
            hv.InDienTich();

            var ht = new clHinhTron();
            ht.Ten = "hình tròn";
            ht.DuongKinh = 10;
            ht.TinhDienTich();
            ht.InDienTich();
        }
    }

    class clHinhVuong : clHinhHoc
    {
        public double Canh { get; set; }
        public override void TinhDienTich()
        {
            DienTich = Canh * Canh;
        }
    }

    class clHinhTron : clHinhHoc
    {
        public double DuongKinh { get; set; }
        public override void TinhDienTich()
        {
            DienTich = Math.PI * DuongKinh * DuongKinh /4;
        }
    }


    public abstract class clHinhHoc
    {
        //Thuộc tính
        public string Ten {  get; set; }
        public double DienTich { get; set; }

        // Hành vi

        public abstract void TinhDienTich();

        public void InDienTich()
        {
            Console.WriteLine("Tên: {0}, Diện tích: {1}", Ten, DienTich);
        }
    }
}
