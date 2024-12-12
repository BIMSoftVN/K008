using Autodesk.Revit.UI;
using RevitApp.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevitApp
{
    class App : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            string TabName = "K008";
            string PanelName = "K008 Panel";
            string AssemblyName = Assembly.GetExecutingAssembly().Location;

            application.CreateRibbonTab(TabName);
            var Panel = application.CreateRibbonPanel(TabName, PanelName);

            var btnData = new PushButtonData("VeTuong", "Vẽ tường", AssemblyName, "RevitApp.DrawWall");
            btnData.ToolTip = "Lệnh vẽ tường";

            string ImgPath = "RevitApp.Photo.BrickWall.png";

            btnData.LargeImage = RersourceToImg.ConvertWithHeight(ImgPath, 32, 32);
            btnData.Image = RersourceToImg.ConvertWithHeight(ImgPath, 16, 16);

            Panel.AddItem(btnData);

            return Result.Succeeded;
        }
    }
}
