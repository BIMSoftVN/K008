using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RevitApp.ImportCAD
{
    [Transaction(TransactionMode.Manual)]
    public class OpenWindow : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiDoc = commandData.Application.ActiveUIDocument;
            var doc = uiDoc.Document;

            var selFilter = new LinkCADFilter(doc);
            var LinkCAD = uiDoc.Selection.PickObject(ObjectType.Element, selFilter, "Chọn file Link CAD");


            var win = new vMain();

           ((vmMain)win.DataContext).LinkCAD = doc.GetElement(LinkCAD.ElementId) as ImportInstance;

            win.ShowDialog();

            return Result.Succeeded;
        }

        public class LinkCADFilter : ISelectionFilter
        {
            Document doc = null;
            public LinkCADFilter(Document document)
            {
                doc = document;
            }

            public bool AllowElement(Element element)
            {
                var res = false;

                try
                {
                    var importIns = (element as ImportInstance);
                    if (importIns.IsLinked == true)
                    {
                        res = true;
                    }    
                }
                catch
                {

                }

                return res;
            }

            public bool AllowReference(Reference refer, XYZ point)
            {
                return false;
            }
        }
    }
}
