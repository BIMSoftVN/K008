using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpf.Core;
using System.Windows;

namespace K008
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            SplashScreenManager.CreateThemed().ShowOnStartup();
        }
    }
}
