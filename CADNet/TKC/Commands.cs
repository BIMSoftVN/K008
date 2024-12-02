using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using Exception = System.Exception;

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

                        //var bList = new List<clBlockInfo>();

                        var win = new vMain();
                        var vmWin = (vmMain)win.DataContext;


                        BlockTable blockTable = tr.GetObject(aDb.BlockTableId, OpenMode.ForRead) as BlockTable;
                        if (blockTable != null)
                        {
                            foreach (var blockId in blockTable)
                            {
                                BlockTableRecord blockRec = tr.GetObject(blockId, OpenMode.ForRead) as BlockTableRecord;
                                if (blockRec != null)
                                {
                                    if (blockRec.Name == "TK_Title")
                                    {
                                        vmWin.TitleId = blockRec.Id;    
                                    }

                                    if (blockRec.Name == "TK_Body")
                                    {
                                        vmWin.BodyId = blockRec.Id;
                                    }
                                }
                            }
                        }


                        
                        vmWin.cocSource.Clear();
                        vmWin.cocSource.AddRange(cocList);

                        //vmWin.BlockSource.Clear();
                        //vmWin.BlockSource.AddRange(bList);

                        win.Show();

                    
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
