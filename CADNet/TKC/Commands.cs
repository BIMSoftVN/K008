using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CADNet.TKC
{
    public class Commands
    {
        [CommandMethod("K008_ThongKeCoc")]
        public static void K008_ThongKeCoc()
        {

            try
            {
                Document aDoc = Application.DocumentManager.MdiActiveDocument;
                Editor aEdit = aDoc.Editor;
                Database aDb = aDoc.Database;

                using (Transaction tr = aDb.TransactionManager.StartTransaction())
                {
                    TypedValue[] Tval = new TypedValue[4];

                    Tval.SetValue(new TypedValue((int)DxfCode.Operator, "<OR"), 0);
                    Tval.SetValue(new TypedValue((int)DxfCode.Start, "CIRCLE"), 1);
                    Tval.SetValue(new TypedValue((int)DxfCode.Start, "TEXT"),2);
                    Tval.SetValue(new TypedValue((int)DxfCode.Operator, "OR>"), 3);

        
                    SelectionFilter filter = new SelectionFilter(Tval);

                    var res = aEdit.GetSelection(filter);
                    if (res.Status == PromptStatus.OK)
                    {
                        var sset = res.Value;

                        List<clCocInfo> cocList = new List<clCocInfo>();
                        List<DBText> textList = new List<DBText>();

                        foreach (SelectedObject e in sset)
                        {
                            var oCir = tr.GetObject(e.ObjectId, OpenMode.ForRead) as Circle;
                            if (oCir !=null)
                            {
                                var coc = new clCocInfo
                                {
                                    Id = oCir.ObjectId,
                                    Layer = oCir.Layer, 
                                    Diameter = oCir.Diameter,
                                    Orgin  = oCir.Center
                                };

                                cocList.Add(coc);
                            }    
                            else
                            {
                                var oText = tr.GetObject(e.ObjectId, OpenMode.ForRead) as DBText;
                                if (oText != null)
                                {
                                    textList.Add(oText);
                                }   
                            }    
                        }

                        if (textList.Count > 0)
                        {
                            foreach (var text in textList)
                            {
                                var cocCheckList = cocList.Where(o => string.IsNullOrEmpty(o.Name) ||
                                string.IsNullOrWhiteSpace(o.Name));

                                if (cocCheckList!=null && cocCheckList.Count()>0)
                                {
                                    var NestedCircle = cocCheckList.OrderBy(o=>o.Orgin.DistanceTo(text.Position)).First();

                                    NestedCircle.Name = text.TextString;
                                    NestedCircle.TextLayer = text.Layer;
                                    NestedCircle.DisTance = NestedCircle.Orgin.DistanceTo(text.Position);
                                } 
                                else
                                {
                                    break;
                                }    
                            }
                        }

                        var win = new vMain();
                        var vmWin = (vmMain)win.DataContext;

                        vmWin.cocSource.Clear();
                        vmWin.cocSource.AddRange(cocList);

                        win.ShowDialog();

                    
                    }
                }    

                
            }
            catch (Exception ex)
            {

            }


            //var win = new vMain();
            //win.ShowDialog();
        }
    }
}
