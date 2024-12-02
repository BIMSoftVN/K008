using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using DevExpress.Utils.About;
using K008Libs.Mvvm;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CADNet.TKC
{
    public class vmMain : PropertyChangedBase
    {
        private ObservableRangeCollection<clCocInfo> _cocSource = new ObservableRangeCollection<clCocInfo>();
        public ObservableRangeCollection<clCocInfo> cocSource
        {
            get
            {
                return _cocSource;
            }
            set
            {
                _cocSource = value;
                OnPropertyChanged();
            }
        }


        //private ObservableRangeCollection<clBlockInfo> _BlockSource = new ObservableRangeCollection<clBlockInfo>();
        //public ObservableRangeCollection<clBlockInfo> BlockSource
        //{
        //    get
        //    {
        //        return _BlockSource;
        //    }
        //    set
        //    {
        //        _BlockSource = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private clBlockInfo _BlockItem = new clBlockInfo();
        //public clBlockInfo BlockItem
        //{
        //    get
        //    {
        //        return _BlockItem;
        //    }
        //    set
        //    {
        //        _BlockItem = value;
        //        OnPropertyChanged();
        //    }
        //}


        private ObjectId _TitleId;
        public ObjectId TitleId
        {
            get
            {
                return _TitleId;
            }
            set
            {
                _TitleId = value;
                OnPropertyChanged();
            }
        }

        private ObjectId _BodyId;
        public ObjectId BodyId
        {
            get
            {
                return _BodyId;
            }
            set
            {
                _BodyId = value;
                OnPropertyChanged();
            }
        }

        private ActionCommand cmd_VeBang;

        public ICommand Cmd_VeBang
        {
            get
            {
                if (cmd_VeBang == null)
                {
                    cmd_VeBang = new ActionCommand(PerformCmd_VeBang);
                }

                return cmd_VeBang;
            }
        }

        private void PerformCmd_VeBang()
        {
            try
            {
                if (TitleId != null && BodyId !=null)
                {
                    Document adoc = Application.DocumentManager.MdiActiveDocument;

                    using (var LocDoc = adoc.LockDocument())
                    {
                        Editor adocEd = adoc.Editor;
                        Database db = adoc.Database;

                        using (Transaction tr = db.TransactionManager.StartTransaction())
                        {
                            try
                            {
                                BlockTable blockTable = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                                if (blockTable != null)
                                {
                                    BlockTableRecord ms = tr.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                                    PromptPointResult p1 = adocEd.GetPoint("Chọn điểm vẽ");
                                    if (p1.Status == PromptStatus.OK)
                                    {
                                        var InsPoint = p1.Value;

                                        BlockReference blockRef = new BlockReference(InsPoint, TitleId);
                                        ms.AppendEntity(blockRef);
                                        tr.AddNewlyCreatedDBObject(blockRef, true);

                                        var bouBox = blockRef.GeometricExtents;
                                        InsPoint = new Point3d(InsPoint.X, InsPoint.Y - (bouBox.MaxPoint.Y - bouBox.MinPoint.Y), InsPoint.Z);

                                        foreach (clCocInfo coc in cocSource)
                                        {
                                            using (BlockReference bodyRef = new BlockReference(InsPoint, BodyId))
                                            {
                                                ms.AppendEntity(bodyRef);
                                                tr.AddNewlyCreatedDBObject(bodyRef, true);

                                                BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                                                BlockTableRecord bodyDef = bt["TK_Body"].GetObject(OpenMode.ForRead) as BlockTableRecord;
                                                foreach (ObjectId id in bodyDef)
                                                {
                                                    var obj = id.GetObject(OpenMode.ForRead);
                                                    var attDef = obj as AttributeDefinition;

                                                    if (attDef != null)
                                                    {
                                                        if (attDef.Tag.ToUpper() == "NAME")
                                                        {
                                                            using (var attRef = new AttributeReference())
                                                            {
                                                                if (attRef != null)
                                                                {
                                                                    attRef.SetAttributeFromBlock(attDef, bodyRef.BlockTransform);
                                                                    if (string.IsNullOrEmpty(coc.Name) || string.IsNullOrWhiteSpace(coc.Name))
                                                                    {
                                                                        coc.Name = "";
                                                                    }

                                                                    attRef.TextString = coc.Name;
                                                                    bodyRef.AttributeCollection.AppendAttribute(attRef);
                                                                    tr.AddNewlyCreatedDBObject(attRef, true);
                                                                }    
                                                            }    
                                                        }

                                                        if (attDef.Tag == "DK")
                                                        {
                                                            using (var attRef = new AttributeReference())
                                                            {
                                                                if (attRef != null)
                                                                {
                                                                    attRef.SetAttributeFromBlock(attDef, bodyRef.BlockTransform);

                                                                    if (coc.Diameter == null)
                                                                    {
                                                                        attRef.TextString = "";
                                                                    }
                                                                    else
                                                                    {
                                                                        attRef.TextString = Math.Round(coc.Diameter.Value,0).ToString();
                                                                    }   
                                                                    bodyRef.AttributeCollection.AppendAttribute(attRef);
                                                                    tr.AddNewlyCreatedDBObject(attRef, true);
                                                                }
                                                            }
                                                        }

                                                        if (attDef.Tag == "X")
                                                        {
                                                            using (var attRef = new AttributeReference())
                                                            {
                                                                if (attRef != null)
                                                                {
                                                                    attRef.SetAttributeFromBlock(attDef, bodyRef.BlockTransform);

                                                                    if (coc.Orgin.X == null)
                                                                    {
                                                                        attRef.TextString = "";
                                                                    }
                                                                    else
                                                                    {
                                                                        attRef.TextString = Math.Round(coc.Orgin.X, 2).ToString();
                                                                    }
                                                                    bodyRef.AttributeCollection.AppendAttribute(attRef);
                                                                    tr.AddNewlyCreatedDBObject(attRef, true);
                                                                }
                                                            }
                                                        }

                                                        if (attDef.Tag == "Y")
                                                        {
                                                            using (var attRef = new AttributeReference())
                                                            {
                                                                if (attRef != null)
                                                                {
                                                                    attRef.SetAttributeFromBlock(attDef, bodyRef.BlockTransform);

                                                                    if (coc.Orgin.Y == null)
                                                                    {
                                                                        attRef.TextString = "";
                                                                    }
                                                                    else
                                                                    {
                                                                        attRef.TextString = Math.Round(coc.Orgin.Y, 2).ToString();
                                                                    }
                                                                    bodyRef.AttributeCollection.AppendAttribute(attRef);
                                                                    tr.AddNewlyCreatedDBObject(attRef, true);
                                                                }
                                                            }
                                                        }
                                                    }    
                                                }    

                                                bouBox = blockRef.GeometricExtents;
                                                InsPoint = new Point3d(InsPoint.X, InsPoint.Y - (bouBox.MaxPoint.Y - bouBox.MinPoint.Y), InsPoint.Z);
                                            }
                                        }
                                    }
                                }

                                tr.Commit();

                                adocEd.Regen();
                            }
                            catch (Exception ex)
                            {
                                tr.Dispose();
                                Application.ShowAlertDialog(ex.Message);
                            }
                        }
                    }                    
                }    
            }
            catch (Exception ex)
            {

            }
        }
    }
}
