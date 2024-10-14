using iTask.Classes;
using iTask.Models;
using iTask.Views.Pages;
using K008Libs.Mvvm;
using K008Libs.Objects;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iTask.ViewModels.Pages
{
    public class vmGiaoViec : PropertyChangedBase
    {
        private ObservableRangeCollection<clTask> _TaskList = new ObservableRangeCollection<clTask>();
        public ObservableRangeCollection<clTask> TaskList
        {
            get
            {
                return _TaskList;
            }
            set
            {
                _TaskList = value;
                OnPropertyChanged();
            }
        }

        private ObservableRangeCollection<clTask> _TaskSelect = new ObservableRangeCollection<clTask>();
        public ObservableRangeCollection<clTask> TaskSelect
        {
            get
            {
                return _TaskSelect;
            }
            set
            {
                _TaskSelect = value;
                OnPropertyChanged();
            }
        }

        private clTask _TaskItem = new clTask();
        public clTask TaskItem
        {
            get
            {
                return _TaskItem;
            }
            set
            {
                _TaskItem = value;
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
                TaskList.Clear();

                var tList = await mEF.GetAllTasks();

                TaskList.AddRange(tList);

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
                    case "pAddTask":
                        vmM.PopUpFrameContent = new pTaskInfo();

                        (vmM.PopUpFrameContent.DataContext as vmTaskInfo).Task = new clTask();

                        vmM.IsPopUp = true;
                        break;

                     case "pEditTask":
                        vmM.PopUpFrameContent = new pTaskInfo();

                        (vmM.PopUpFrameContent.DataContext as vmTaskInfo).Task = TaskItem.ShallowCopy();

                        vmM.IsPopUp = true;
                        break;

                }
            }
            catch
            {

            }
        }
    }
}
