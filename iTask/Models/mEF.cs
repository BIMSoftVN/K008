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

        public static async Task<List<clTask>> GetAllTasks()
        {
            List<clTask> uList = null;

            try
            {
                if (App.dbContext.Database.CanConnect())
                {
                    uList = await App.dbContext.Tasks.Include(o=>o.NguoiGiao).Include(o => o.NguoiNhan).AsNoTracking().ToListAsync();
                }
            }
            catch
            {

            }

            return uList;
        }

        public static async Task<bool> AddTask(clTask task)
        {
            var res = false;

            try
            {
                if (App.dbContext.Database.CanConnect())
                {
                    var ngUser = await (from p in App.dbContext.Users where (p.Id == task.NguoiGiaoId) select p).FirstOrDefaultAsync();
                    var nnUser = await (from p in App.dbContext.Users where (p.Id == task.NguoiNhanId) select p).FirstOrDefaultAsync();

                    var task_Out = task.ShallowCopy();
                    task_Out.NguoiGiao = ngUser;
                    task_Out.NguoiNhan = nnUser;

                    App.dbContext.Tasks.Add(task_Out);
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

        public static async Task<bool> UpdateTask(clTask task)
        {
            var res = false;
            clTask task_out = null;

            try
            {
                if (App.dbContext.Database.CanConnect())
                {
                    task_out = await App.dbContext.Tasks.FindAsync(task.Id);
                    if (task_out != null)
                    {
                        task_out.Ten = task.Ten;
                        task_out.MoTa = task.MoTa;
                        task_out.NgayTao = task.NgayTao;
                        task_out.Deadline = task.Deadline;
                        task_out.TrangThai = task.TrangThai;
                        task_out.NguoiGiaoId = task.NguoiGiaoId;
                        task_out.NguoiNhanId = task.NguoiNhanId;

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

        public static async Task<bool> DeleteTask(List<clTask> taskList)
        {
            var res = false;
            clUser user_out = null;

            try
            {
                if (App.dbContext.Database.CanConnect())
                {
                    var idList = taskList.Select(x => x.Id).ToList();
                    var delDb = await App.dbContext.Tasks.Where(x => idList.Contains(x.Id)).ToListAsync();

                    App.dbContext.Tasks.RemoveRange(delDb);
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

    }
}
