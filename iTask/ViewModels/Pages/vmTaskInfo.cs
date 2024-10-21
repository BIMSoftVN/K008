using iTask.Classes;
using iTask.Models;
using K008Libs.Mvvm;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iTask.ViewModels.Pages
{
    public class vmTaskInfo : PropertyChangedBase
    {
        private clTask _Task;
        public clTask Task
        {
            get
            {
                return _Task;
            }
            set
            {
                _Task = value;
                OnPropertyChanged();
            }
        }

        private ObservableRangeCollection<clUser> _UserList = new ObservableRangeCollection<clUser>();
        public ObservableRangeCollection<clUser> UserList
        {
            get
            {
                return _UserList;
            }
            set
            {
                _UserList = value;
                OnPropertyChanged();
            }
        }


        private ActionCommand cmd_LoadAll;

        public ICommand Cmd_LoadAll
        {
            get
            {
                if (cmd_LoadAll == null)
                {
                    cmd_LoadAll = new ActionCommand(PerformCmd_LoadAll);
                }

                return cmd_LoadAll;
            }
        }

        private async void PerformCmd_LoadAll()
        {
            try
            {
                UserList.Clear();

                var uList = await mEF.GetAllUsers();

                if (Task.NguoiGiaoId!=null)
                {
                    Task.NguoiGiao = uList.Where(o => o.Id == Task.NguoiGiaoId).FirstOrDefault();
                }

                if (Task.NguoiNhanId != null)
                {
                    Task.NguoiNhan = uList.Where(o => o.Id == Task.NguoiNhanId).FirstOrDefault();
                }

                UserList.AddRange(uList);

            }
            catch
            {

            }
        }

        private ActionCommand cmd_UpdateData;

        public ICommand Cmd_UpdateData
        {
            get
            {
                if (cmd_UpdateData == null)
                {
                    cmd_UpdateData = new ActionCommand(PerformCmd_UpdateData);
                }

                return cmd_UpdateData;
            }
        }

        private async void PerformCmd_UpdateData()
        {
            try
            {
                bool IsSuccess = false;

                if (this.Task.Id > 0 )
                {
                    IsSuccess = await mEF.UpdateTask(this.Task);
                }
                else
                {
                    IsSuccess = await mEF.AddTask(this.Task);

                }


                if (IsSuccess)
                {
                    var vmM = (App.Current.MainWindow.DataContext as vmMain);
                    vmM.IsPopUp = false;
                    vmM.PopUpFrameContent = null;

                    (vmM.MainFrameContent.DataContext as vmGiaoViec).Cmd_LoadAll.Execute(null);
                }
            }
            catch
            {

            }
        }
    }
}
