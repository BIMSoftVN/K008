using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
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
                Document adoc = Application.DocumentManager.MdiActiveDocument;
                Editor adocEd = adoc.Editor;
                Database db = adoc.Database;

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                    string TK_Title = "TK_Title";
                    string TK_Body = "TK_Body";


                    if (bt.Has(TK_Title))
                    {
                        BlockTableRecord ms = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                        var ppr1 = adocEd.GetPoint("Chọn điểm bắt đầu vẽ");
                        if (ppr1.Status == PromptStatus.OK)
                        {
                            var InsPoint = ppr1.Value;

                            BlockTableRecord btr = bt[TK_Title].GetObject(OpenMode.ForRead) as BlockTableRecord;
                            BlockReference oBlock = new BlockReference(InsPoint, btr.Id);
                            ms.AppendEntity(oBlock);
                            tr.AddNewlyCreatedDBObject(oBlock, true);

                            //foreach (var id in btr.GetBlockReferenceIds(true, false))
                            //{
                            //    Application.ShowAlertDialog($"Block Reference ObjectId: {id}");
                            //    //BlockReference oBlock = new BlockReference(InsPoint, id);
                            //    //ms.AppendEntity(oBlock);
                            //    //tr.AddNewlyCreatedDBObject(oBlock, true);

                            //    Circle text = new Circle();
                            //    ms.AppendEntity(text);
                            //    tr.AddNewlyCreatedDBObject(text, true);
                            //};
                        }
                    }


                    tr.Commit();
                }    


                    
            }
            catch (Exception ex)
            {

            }
        }
    }
}
