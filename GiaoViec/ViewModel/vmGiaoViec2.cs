using GiaoViec.Libs;
using GiaoViec.Views;
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

        private Page _PopUpFrameContent;
        public Page PopUpFrameContent
        {
            get
            {
                return _PopUpFrameContent;
            }
            set
            {
                _PopUpFrameContent = value;
                OnPropertyChanged();
            }
        }

        private bool _IsPopUp = false;
        public bool IsPopUp
        {
            get
            {
                return _IsPopUp;
            }
            set
            {
                _IsPopUp = value;
                OnPropertyChanged();
            }
        }


        private ActionCommand _Cmd_OpenPage;

        public ICommand Cmd_OpenPage
        {
            get
            {
                if (_Cmd_OpenPage == null)
                {
                    _Cmd_OpenPage = new ActionCommand(PerformCmd_OpenPage);
                }

                return _Cmd_OpenPage;
            }
        }

        private void PerformCmd_OpenPage(object parameter)
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

                    case "pThongTinTaiKhoan":
                        PopUpFrameContent = new pThongTinTaiKhoan();
                        IsPopUp = true;
                        break;
                }
            }
            catch
            {

            }
        }

        private ActionCommand cmd_ClosePopUp;

        public ICommand Cmd_ClosePopUp
        {
            get
            {
                if (cmd_ClosePopUp == null)
                {
                    cmd_ClosePopUp = new ActionCommand(PerformCmd_ClosePopUp);
                }

                return cmd_ClosePopUp;
            }
        }

        private void PerformCmd_ClosePopUp()
        {
            PopUpFrameContent = null;
            IsPopUp = false;
        }
    }
}
