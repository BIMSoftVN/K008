using GiaoViec.Views.Pages;
using GiaoViec.Views;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GiaoViec.Classes;
using GiaoViec.Libs;
using DevExpress.XtraPrinting.Native.WebClientUIControl;
using Newtonsoft.Json;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace GiaoViec.ViewModel.Pages
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
            }
        }




        private ActionCommand cmd_OpenFrame;

        public ICommand Cmd_OpenFrame
        {
            get
            {
                if (cmd_OpenFrame == null)
                {
                    cmd_OpenFrame = new ActionCommand(PerformCmd_OpenFrame);
                }

                return cmd_OpenFrame;
            }
        }

        private void PerformCmd_OpenFrame(object parameter)
        {
            string ActionName = parameter as string;
            try
            {
                var vmMain = App.Current.MainWindow.DataContext as vmGiaoViec2;

                switch (ActionName)
                {
                    case "AddTask":
                        vmMain.PopUpFrameContent = new pAddTask();
                        vmMain.IsPopUp = true;
                        break;
                }
            }
            catch
            {

            }
        }

        private ActionCommand cmd_Save;

        public ICommand Cmd_Save
        {
            get
            {
                if (cmd_Save == null)
                {
                    cmd_Save = new ActionCommand(PerformCmd_Save);
                }

                return cmd_Save;
            }
        }

        private void PerformCmd_Save()
        {
            Properties.Settings.Default.SaveData = JsonConvert.SerializeObject(TaskList);
            Properties.Settings.Default.Save();
            MessageBox.Show("Đã lưu dữ liệu");
        }

        private ActionCommand cmd_LoadData;

        public ICommand Cmd_LoadData
        {
            get
            {
                if (cmd_LoadData == null)
                {
                    cmd_LoadData = new ActionCommand(PerformCmd_LoadData);
                }

                return cmd_LoadData;
            }
        }

        private void PerformCmd_LoadData()
        {
            try
            {
                TaskList.Clear();
                List<clTask> tList = JToken.Parse(Properties.Settings.Default.SaveData).ToObject<List<clTask>>();
                TaskList.AddRange(tList);
            }
            catch
            {

            }
            
        }
    }
}
