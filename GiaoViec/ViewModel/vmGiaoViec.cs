using GiaoViec.Classes;
using GiaoViec.Libs;
using GiaoViec.Views;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GiaoViec.ViewModel
{
    public class vmGiaoViec
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



        private ActionCommand cmd_ThemCongViec;

        public ICommand Cmd_ThemCongViec
        {
            get
            {
                if (cmd_ThemCongViec == null)
                {
                    cmd_ThemCongViec = new ActionCommand(PerformCmd_ThemCongViec);
                }

                return cmd_ThemCongViec;
            }
        }

        private void PerformCmd_ThemCongViec()
        {
            var win = new vTaoGiaoViec();
            win.ShowDialog();
            var task = ((vmTaoGiaoViec)win.DataContext).Task;
            task.NgayTao = DateTime.Now;
            TaskList.Add(task);
        }

        
    }
}
