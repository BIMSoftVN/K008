using Autodesk.Revit.Attributes;
using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RevitApp
{

    [Transaction(TransactionMode.Manual)]
    public class DrawWall : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiDoc = commandData.Application.ActiveUIDocument;
            var doc = uiDoc.Document;

            XYZ ep1 = new XYZ(0, 0, 0);
            XYZ ep2 = new XYZ(5000, 0, 0);

            Line wLine = Line.CreateBound(ep1, ep2);

            FilteredElementCollector Collector =  new FilteredElementCollector(doc);
            Collector.OfClass(typeof(WallType));

            var wType = Collector.FirstElement() as WallType;
            var aView = uiDoc.ActiveView as View;
            var level = aView.GenLevel as Level;

            var height = 2500;
            var offset = 0;

            using (Transaction trans = new Transaction(doc, "DrawWall"))
            {
                trans.Start();
                Wall.Create(doc, wLine, wType.Id, level.Id, height, offset, false, true);
                trans.Commit();
            }    


            return Result.Succeeded;
        }


    }
}
