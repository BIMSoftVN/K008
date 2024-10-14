using DevExpress.Mvvm;
using iTask.Views.Pages;
using K008Libs.Mvvm;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace iTask.ViewModels
{
    public class vmMain : PropertyChangedBase
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

        private async void PerformCmd_OpenPage(object parameter)
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
                        this.PopUpFrameContent = new pAccInfo();
                        this.IsPopUp = true;
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



        private ActionCommand cmd_SetLang;

        public ICommand Cmd_SetLang
        {
            get
            {
                if (cmd_SetLang == null)
                {
                    cmd_SetLang = new ActionCommand(PerformCmd_SetLang);
                }

                return cmd_SetLang;
            }
        }

        private void PerformCmd_SetLang(object parameter)
        {
            Properties.Settings.Default.LangName = (parameter as string);
            Properties.Settings.Default.Save();

            Process.Start(Assembly.GetExecutingAssembly().Location);
            Environment.Exit(0);
        }
    }
}
