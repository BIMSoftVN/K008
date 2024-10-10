using iTask.Classes;
using iTask.Models;
using K008Libs.Mvvm;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Excel = Microsoft.Office.Interop.Excel;

namespace iTask.ViewModels.Pages
{
    public class vmImportExcel : PropertyChangedBase
    {
        private string _FilePath =null;
        public string FilePath
        {
            get
            {
                return _FilePath;
            }
            set
            {
                _FilePath = value;
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





        private ActionCommand cmd_SelectExcelFile;

        public ICommand Cmd_SelectExcelFile
        {
            get
            {
                if (cmd_SelectExcelFile == null)
                {
                    cmd_SelectExcelFile = new ActionCommand(PerformCmd_SelectExcelFile);
                }

                return cmd_SelectExcelFile;
            }
        }

        private void PerformCmd_SelectExcelFile()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Multiselect = false;
                dlg.Filter = "Excel files (*.xlsx)|*.xlsx";

                if (dlg.ShowDialog() ==true &&
                    File.Exists(dlg.FileName))
                {
                    FilePath = dlg.FileName;
                    ReadExcelFile(FilePath);
                }    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void ReadExcelFile(string filePath)
        {
            UserList.Clear();

            var ExcelApp = new Excel.Application();
            ExcelApp.Visible = true;

            var wb = ExcelApp.Workbooks.Open(filePath);
            Excel.Worksheet ws = wb.Sheets["Sheet1"];

            long lr = ((Excel.Range)ws.Cells[ws.Rows.Count, 1]).End[XlDirection.xlUp].Row;

            var tlist = new List<clUser>();

            for (long i = 2; i <= lr; i++) 
            {
                DateTime? dt = null;
                try
                {
                    dt = ws.Range["C" + i].Value;
                }
                catch
                {
                    try
                    {
                        dt = DateTime.ParseExact(ws.Range["C" + i].Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        
                    }
                }

                var user = new clUser
                {
                    Email = ws.Range["A" + i].Value,
                    UserName = ws.Range["B" + i].Value,
                    DateOfBirth = dt,
                    Address = ws.Range["D" + i].Value
                };

                tlist.Add(user);
            }

            UserList.AddRange(tlist);

            wb.Close();
            wb = null;
            ExcelApp=null;

            
        }

        private ActionCommand cmd_Import;

        public ICommand Cmd_Import
        {
            get
            {
                if (cmd_Import == null)
                {
                    cmd_Import = new ActionCommand(PerformCmd_Import);
                }

                return cmd_Import;
            }
        }

        private async void PerformCmd_Import()
        {
            try
            {
                bool IsSuccess = await mEF.ImportUsers(UserList.ToList());


                if (IsSuccess)
                {
                    var vmM = (App.Current.MainWindow.DataContext as vmMain);
                    vmM.IsPopUp = false;
                    vmM.PopUpFrameContent = null;

                    (vmM.MainFrameContent.DataContext as vmNguoiDung).Cmd_LoadAll.Execute(null);
                }
            }
            catch
            {

            }
        }
    }
}
