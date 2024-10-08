﻿using DevExpress.Xpf.Core;
using iTask.EF;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace iTask
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static CultureInfo cul = new CultureInfo(iTask.Properties.Settings.Default.LangName);

        public static string UserId = "e4e0ddbb-edf4-4f20-810a-05bd2cff92a0";

        public static string ConnectionString { get; set; } = "Server=./;Database=iTask;TrustServerCertificate=True;MultipleActiveResultSets=True;";

        public static DbContextOptionsBuilder<iTaskDbContext> op = new DbContextOptionsBuilder<iTaskDbContext>();
        public static iTaskDbContext dbContext;

        protected override void OnStartup(StartupEventArgs e)
        {
            ApplicationThemeHelper.ApplicationThemeName = Theme.Win11LightName;
            base.OnStartup(e);

            Thread.CurrentThread.CurrentCulture = cul;
            Thread.CurrentThread.CurrentUICulture = cul;

            CultureInfo.DefaultThreadCurrentCulture = cul;
            CultureInfo.DefaultThreadCurrentUICulture = cul;

            op.UseSqlServer(App.ConnectionString);
            dbContext = new iTaskDbContext(op.Options);
        }
    }
}
