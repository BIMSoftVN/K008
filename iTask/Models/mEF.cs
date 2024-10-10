using iTask.Classes;
using iTask.EF;
using K008Libs.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTask.Models
{
    internal class mEF
    {
        public static async Task<List<clUser>> GetAllUsers()
        {
            List<clUser> uList = null;

            try
            {
                if (App.dbContext.Database.CanConnect())
                {
                    uList = await App.dbContext.Users.AsNoTracking().ToListAsync();
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
                if (App.dbContext.Database.CanConnect())
                {
                    var user_Out = user.ShallowCopy();
                    user_Out.Id = Guid.NewGuid().ToString();
                    App.dbContext.Users.Add(user_Out);
                    var kq = await App.dbContext.SaveChangesAsync();
                    if (kq>0)
                    {
                        res = true;
                    }    
                }
            }
            catch (Exception ex)
            {

            }

            return res;
        }

        public static async Task<bool> UpdateUser(clUser user)
        {
            var res = false;
            clUser user_out = null;

            try
            {
                if (App.dbContext.Database.CanConnect())
                {
                    user_out = await App.dbContext.Users.FindAsync(user.Id);
                    if (user_out!=null)
                    {
                        user_out.UserName = user.UserName;
                        user_out.Email = user.Email;
                        user_out.Address = user.Address;
                        user_out.DateOfBirth = user.DateOfBirth;
                        user_out.Photo = user.Photo;

                        var kq = await App.dbContext.SaveChangesAsync();
                        if (kq > 0)
                        {
                            res = true;
                        }
                    }
                }       
            }
            catch
            {

            }

            return res;
        }

        public static async Task<bool> DeleteUser(List<clUser> userList)
        {
            var res = false;
            clUser user_out = null;

            try
            {
                if (App.dbContext.Database.CanConnect())
                {
                    App.dbContext.Users.RemoveRange(userList);
                    var kq = await App.dbContext.SaveChangesAsync();
                    if (kq > 0) 
                    {
                        res = true;
                    }
                }
            }
            catch
            {

            }

            return res;
        }

        public static async Task<bool> ImportUsers(List<clUser> UserList)
        {
            var res = false;

            try
            {
                if (App.dbContext.Database.CanConnect())
                {
                    foreach (var user in UserList)
                    {
                        user.Id = Guid.NewGuid().ToString();
                    }    

                    App.dbContext.Users.AddRange(UserList);
                    var kq = await App.dbContext.SaveChangesAsync();
                    if (kq > 0)
                    {
                        res = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return res;
        }

    }
}
