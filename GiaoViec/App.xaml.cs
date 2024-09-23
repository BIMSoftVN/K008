using DevExpress.Xpf.Core;
using GiaoViec.Views.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GiaoViec
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static Page _preloadedWindow;
        static App()
        {
            CompatibilitySettings.UseThemedWindowInServices = true;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            ApplicationThemeHelper.ApplicationThemeName = Theme.Win11LightName;
            base.OnStartup(e);
            _preloadedWindow = new pGiaoViec();
            var gridControl = new DevExpress.Xpf.Grid.GridControl();

        }
    }
}
