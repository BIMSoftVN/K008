using GiaoViec.Classes;
using GiaoViec.Libs;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GiaoViec.ViewModel.Pages
{
    public class vmAddTask : PropertyChangedBase
    {
        private clTask _Task = new clTask();
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

        private ActionCommand cmd_ThemCV;

        public ICommand Cmd_ThemCV
        {
            get
            {
                if (cmd_ThemCV == null)
                {
                    cmd_ThemCV = new ActionCommand(PerformCmd_ThemCV);
                }

                return cmd_ThemCV;
            }
        }

        private void PerformCmd_ThemCV()
        {
            var vmMain = (App.Current.MainWindow.DataContext as vmGiaoViec2);
            var vmpGiaoViec = (vmMain.MainFrameContent.DataContext as Pages.vmGiaoViec);
            vmpGiaoViec.TaskList.Add(Task);
            vmMain.IsPopUp = false;
        }
    }
}
