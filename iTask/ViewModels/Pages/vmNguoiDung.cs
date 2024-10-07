using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using iTask.Classes;
using iTask.Models;
using iTask.Views.Pages;
using K008Libs.Mvvm;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using K008Libs.Objects;
using System.Windows;
using DevExpress.Xpf.Core;

namespace iTask.ViewModels.Pages
{
    public class vmNguoiDung : PropertyChangedBase
    {
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

        private ObservableRangeCollection<clUser> _UserSelect = new ObservableRangeCollection<clUser>();
        public ObservableRangeCollection<clUser> UserSelect
        {
            get
            {
                return _UserSelect;
            }
            set
            {
                _UserSelect = value;
                OnPropertyChanged();
            }
        }



        private clUser _UserItem = new clUser();
        public clUser UserItem
        {
            get
            {
                return _UserItem;
            }
            set
            {
                _UserItem = value;
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

                UserList.AddRange(uList);

            }
            catch
            {

            }
        }

        private ActionCommand cmd_Popup;

        public ICommand Cmd_Popup
        {
            get
            {
                if (cmd_Popup == null)
                {
                    cmd_Popup = new ActionCommand(PerformCmd_Popup);
                }

                return cmd_Popup;
            }
        }

        private void PerformCmd_Popup(object parameter)
        {
            string ActionName = parameter as string;
            try
            {
                var vmM = (App.Current.MainWindow.DataContext as vmMain);

                switch (ActionName)
                {
                    case "pAddUser":
                        vmM.PopUpFrameContent = new pUserInfo();

                        (vmM.PopUpFrameContent.DataContext as vmUserInfo).User = new clUser();

                        vmM.IsPopUp = true;
                        break;

                    case "pEditUser":
                        vmM.PopUpFrameContent = new pUserInfo();

                        (vmM.PopUpFrameContent.DataContext as vmUserInfo).User = UserItem.ShallowCopy();
                        vmM.IsPopUp = true;
                        break;
                }
            }
            catch
            {

            }
        }

        private ActionCommand cmd_Delete;

        public ICommand Cmd_Delete
        {
            get
            {
                if (cmd_Delete == null)
                {
                    cmd_Delete = new ActionCommand(PerformCmd_Delete);
                }

                return cmd_Delete;
            }
        }

        private void PerformCmd_Delete()
        {
            try
            {
                var kqTB = ThemedMessageBox.Show("Cảnh báo xóa", "Bạn muốn xóa " + UserSelect.Count + " đối tượng", MessageBoxButton.YesNo);
                
                if (kqTB == MessageBoxResult.Yes)
                {
                    var kq = mEF.DeleteUser(UserSelect.ToList());
                    PerformCmd_LoadAll();
                }    
            }
            catch
            {

            }
        }
    }
}
