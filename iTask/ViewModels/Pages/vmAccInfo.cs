using iTask.Classes;
using iTask.Models;
using K008Libs.Mvvm;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace iTask.ViewModels.Pages
{
    internal class vmAccInfo : PropertyChangedBase
    {
        private clUser _User;
        public clUser User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
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
                this.User = await mUser.GetUserById(App.UserId);
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
                var kq = await mUser.UpdateUser(this.User);
                if (kq==true)
                {
                    (App.Current.MainWindow.DataContext as vmMain).IsPopUp = false;
                }    
            }
            catch
            {

            }
        }
    }
}
