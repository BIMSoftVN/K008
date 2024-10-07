using DevExpress.Xpf.Core.Native;
using iTask.Classes;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace iTask.Models
{
    [TestFixture]
    public class mSQLServer
    {
        [Test]
        public static async Task Test()
        {
            var kq = await GetAllUsers();
            Console.WriteLine(kq[1].UserName);
        }

        public static async Task<List<clUser>> GetAllUsers()
        {
            var uList = new List<clUser>();

            try
            {
                using (var conn = new SqlConnection(App.ConnectionString)) 
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM [Users]";
                        var reader = await cmd.ExecuteReaderAsync();
                        var dt = new DataTable();
                        dt.Load(reader);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            uList = JToken.FromObject(dt).ToObject<List<clUser>>();
                        }
                    }
                }
            }
            catch
            {

            }

            return uList;
        }

        public static async Task<bool> AddUser(clUser user)
        {
            var res = false;

            try
            {
                using (var conn = new SqlConnection(App.ConnectionString))
                {
                    conn.Open();

                    using (var trans = conn.BeginTransaction())
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            try
                            {
                                cmd.Parameters.AddWithValue("Id", Guid.NewGuid().ToString());
                                cmd.Parameters.AddWithValue("UserName", user.UserName ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("Email", user.Email ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("DateOfBirth", user.DateOfBirth ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("Address", user.Address ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("Photo", user.Photo ?? (object)DBNull.Value);

                                cmd.CommandText = "INSERT INTO [Users]([Id],[UserName],[Email],[DateOfBirth], [Address], [Photo]) VALUES (@Id,@UserName,@Email,@DateOfBirth, @Address, CONVERT(varbinary(max), @Photo))";
                                var kq = await cmd.ExecuteNonQueryAsync();
                                if (kq > 0)
                                {
                                    res = true; 
                                }

                                trans.Commit();
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                            }
                            
                        }
                    }    
                }
            }
            catch
            {

            }

            return res;
        }

        public static async Task<bool> UpdateUser(clUser user)
        {
            var res = false;

            try
            {
                using (var conn = new SqlConnection(App.ConnectionString))
                {
                    conn.Open();

                    using (var trans = conn.BeginTransaction())
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            try
                            {
                                cmd.Parameters.AddWithValue("Id", user.Id);
                                cmd.Parameters.AddWithValue("UserName", user.UserName ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("Email", user.Email ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("DateOfBirth", user.DateOfBirth ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("Address", user.Address ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("Photo", user.Photo ?? (object)DBNull.Value);

                                cmd.CommandText = "UPDATE [Users] SET [UserName]=@UserName,[Email]=@Email,[DateOfBirth]=@DateOfBirth, [Address]=@Address, [Photo]=CONVERT(varbinary(max), @Photo) WHERE [Id]=@Id";
                                var kq = await cmd.ExecuteNonQueryAsync();
                                if (kq > 0)
                                {
                                    res = true;
                                }

                                trans.Commit();
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                            }

                        }
                    }
                }
            }
            catch
            {

            }

            return res;
        }
    }
}
