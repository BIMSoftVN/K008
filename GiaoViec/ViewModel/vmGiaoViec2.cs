using GiaoViec.Libs;
using GiaoViec.Views.Pages;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace GiaoViec.ViewModel
{
    public class vmGiaoViec2 : PropertyChangedBase
    {
        private Page _MainFrameContent;
        public Page MainFrameContent
        {
            get
            {
                return _MainFrameContent;
            }
            set
            {
                _MainFrameContent = value;
                OnPropertyChanged();
            }
        }


        private ActionCommand cmd_OpenMainPage;

        public ICommand Cmd_OpenMainPage
        {
            get
            {
                if (cmd_OpenMainPage == null)
                {
                    cmd_OpenMainPage = new ActionCommand(PerformCmd_OpenMainPage);
                }

                return cmd_OpenMainPage;
            }
        }

        private void PerformCmd_OpenMainPage(object parameter)
        {
            string ActionName = parameter as string;
            try
            {
                switch (ActionName) 
                {
                    case "pGiaoViec":
                        MainFrameContent = new pGiaoViec();
                        break;

                    case "pNguoiDung":
                        MainFrameContent = new pNguoiDung();
                        break;
                }
            }
            catch
            {

            }
        }
    }
}
