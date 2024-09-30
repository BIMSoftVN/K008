using iTask.Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Diagnostics;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace iTask.Models
{
    [TestFixture]
    public class mUser
    {


        //public static async Task Test()
        //{
        //    Stopwatch stopwatch = Stopwatch.StartNew();

        //    stopwatch.Start();

        //    var t1 = Task.Run(() =>
        //    {
        //        CongTacA();
        //    });

        //    var t2 = Task.Run(() =>
        //    {
        //        CongTacB();
        //    });

        //    var t3 = Task.Run(() =>
        //    {
        //        CongTacC();
        //    });

        //    await Task.WhenAll(t1, t2, t3);


        //    stopwatch.Start();
        //    Console.WriteLine(stopwatch.ElapsedMilliseconds);
        //}

        //public static void CongTacA()
        //{
        //    Thread.Sleep(1000);
        //    Console.WriteLine("Công tác A");
        //}

        //public static void CongTacB()
        //{
        //    Thread.Sleep(1000);
        //    Console.WriteLine("Công tác B");
        //}

        //public static void CongTacC()
        //{
        //    Thread.Sleep(1000);
        //    Console.WriteLine("Công tác C");
        //}


        [Test]
        public static void Test2()
        {
            var kq = GetUserById("e4e0ddbb-edf4-4f20-810a-05bd2cff92a0");

            Console.WriteLine(kq.Result.UserName);
        }

        public static async Task<clUser> GetUserById(string UserId)
        {
            clUser user = new clUser();

            string dbPath = @"C:\Users\kysudo\Desktop\iTaskDataBase.db";
            string connString = "Data Source=" + dbPath + ";Version=3;";

            using (SQLiteConnection conn = new SQLiteConnection(connString)) 
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("id", UserId);
                    cmd.CommandText = "SELECT * FROM UserInfo WHERE [Id]=@id";
                    var reader = await cmd.ExecuteReaderAsync();
                    var dt = new DataTable();
                    dt.Load(reader);

                    if (dt!=null && dt.Rows.Count>0)
                    {
                        user = JToken.FromObject(dt).ToObject<List<clUser>>().FirstOrDefault();
                    }    
                }
            }

            return user;
        }

        public static async Task<bool> UpdateUser(clUser user)
        {
            var res = false;
            string dbPath = @"C:\Users\kysudo\Desktop\iTaskDataBase.db";
            string connString = "Data Source=" + dbPath + ";Version=3;";

            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("Id", user.Id);
                    cmd.Parameters.AddWithValue("Email", user.Email);
                    cmd.Parameters.AddWithValue("UserName", user.UserName);
                    cmd.Parameters.AddWithValue("DateOfBirth", user.DateOfBirth);
                    cmd.Parameters.AddWithValue("Address", user.Address);
                    cmd.Parameters.AddWithValue("Photo", user.Photo);

                    cmd.CommandText = "UPDATE [UserInfo] SET [Email]=@Email,[Photo]=@Photo, [UserName]=@UserName, [DateOfBirth]=@DateOfBirth , [Address]=@Address WHERE [Id]=@Id";
                    var kq = await cmd.ExecuteNonQueryAsync();
                    if (kq>0)
                    {
                        res = true;
                    }    
                }
            }

            return res;
        }
    }
}
