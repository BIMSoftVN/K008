using DevExpress.CodeParser;
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
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Excel = Microsoft.Office.Interop.Excel;

namespace iTask.ViewModels.Pages
{
    public class vmGiaoViec : PropertyChangedBase
    {

        private ObservableRangeCollection<clTTChart> _TTList = new ObservableRangeCollection<clTTChart>();
        public ObservableRangeCollection<clTTChart> TTList
        {
            get
            {
                return _TTList;
            }
            set
            {
                _TTList = value;
                OnPropertyChanged();
            }
        }


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

                DrawChart();

            }
            catch
            {

            }
        }

        private void DrawChart()
        {
            try
            {
                TTList.Clear();
                TTList.Add(new clTTChart
                {
                    TrangThai = "Chưa thực hiện",
                    Color = Brushes.Gray,
                    GiaTri = TaskList.Where(o => o.TrangThai == TrangThai.ChuaThucHien).Count()
                });

                TTList.Add(new clTTChart
                {
                    TrangThai = "Đang thực hiện",
                    Color = Brushes.Blue,
                    GiaTri = TaskList.Where(o => o.TrangThai == TrangThai.DangThucHien).Count()
                });

                TTList.Add(new clTTChart
                {
                    TrangThai = "Hoàn thành",
                    Color = Brushes.Green,
                    GiaTri = TaskList.Where(o => o.TrangThai == TrangThai.HoanThanh).Count()
                });

                TTList.Add(new clTTChart
                {
                    TrangThai = "Hủy bỏ",
                    Color = Brushes.Red,
                    GiaTri = TaskList.Where(o => o.TrangThai == TrangThai.HuyBo).Count()
                });
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

        private async void PerformCmd_Delete()
        {
            try
            {
                var kq = await mEF.DeleteTask(TaskSelect.ToList());
                if (kq == true)
                {
                    PerformCmd_LoadAll();
                }    
            }
            catch
            {

            }
        }

        private ActionCommand cmd_Export;

        public ICommand Cmd_Export
        {
            get
            {
                if (cmd_Export == null)
                {
                    cmd_Export = new ActionCommand(PerformCmd_Export);
                }

                return cmd_Export;
            }
        }

        private void PerformCmd_Export()
        {
            try
            {
                if (TaskSelect!=null && TaskSelect.Count>0)
                {
                    var ExcelApp = new Excel.Application();
                    ExcelApp.Visible = true;

                    var wb = ExcelApp.Workbooks.Add();
                    Excel.Worksheet ws = wb.Sheets[1];

                    long i = 1;

                    ws.Range["A"+i].Value = "STT";
                    ws.Range["B" + i].Value = "Tên công tác";
                    ws.Range["C" + i].Value = "Trạng Thái";
                    ws.Range["D" + i].Value = "Người giao";
                    ws.Range["E" + i].Value = "Người nhận";

                    foreach (var task in TaskSelect)
                    {
                        i++;
                        ws.Range["A" + i].Value = i-1;
                        ws.Range["B" + i].Value = task.Ten;
                        ws.Range["C" + i].Value = task.TrangThai.ToString();
                        ws.Range["D" + i].Value = task.NguoiGiao.UserName;
                        ws.Range["E" + i].Value = task.NguoiNhan.UserName;
                    }
                }   
                else
                {
                    MessageBox.Show("Hãy chọn công tác");
                }    
            }
            catch (Exception ex)
            {

            }
        }
    }
}
