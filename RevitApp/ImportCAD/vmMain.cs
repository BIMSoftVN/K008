using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using K008Libs.Mvvm;
using Microsoft.Xaml.Behaviors.Core;
using RevitApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RevitApp.ImportCAD
{
    public class vmMain : PropertyChangedBase
    {
        private ImportInstance _LinkCAD = null;
        public ImportInstance LinkCAD
        {
            get
            {
                return _LinkCAD;
            }
            set
            {
                _LinkCAD = value;
                OnPropertyChanged();
            }
        }


        private ObservableRangeCollection<clPileFamilyInfo> _PileFamilyList = new ObservableRangeCollection<clPileFamilyInfo>();
        public ObservableRangeCollection<clPileFamilyInfo> PileFamilyList
        {
            get
            {
                return _PileFamilyList;
            }
            set
            {
                _PileFamilyList = value;
                OnPropertyChanged();
            }
        }

        private ObservableRangeCollection<clCircelCADInfo> _CircleList = new ObservableRangeCollection<clCircelCADInfo>();
        public ObservableRangeCollection<clCircelCADInfo> CircleList
        {
            get
            {
                return _CircleList;
            }
            set
            {
                _CircleList = value;
                OnPropertyChanged();
            }
        }





        private clPileFamilyInfo _PileFamilySelect = new clPileFamilyInfo();
        public clPileFamilyInfo PileFamilySelect
        {
            get
            {
                return _PileFamilySelect;
            }
            set
            {
                _PileFamilySelect = value;
                OnPropertyChanged();
            }
        }



        private ActionCommand loadAllCmd;

        public ICommand LoadAllCmd
        {
            get
            {
                if (loadAllCmd == null)
                {
                    loadAllCmd = new ActionCommand(PerformLoadAllCmd);
                }

                return loadAllCmd;
            }
        }

        private void PerformLoadAllCmd()
        {
            PileFamilyList.Clear();
            CircleList.Clear(); 

            try
            {
                var doc = LinkCAD.Document;

                FilteredElementCollector collector = new FilteredElementCollector(doc);
                var Elements = collector.OfCategory(BuiltInCategory.OST_StructuralFoundation).OfClass(typeof(FamilySymbol)).ToElements();

                var DisplayLengthUnitType = doc.GetUnits().GetFormatOptions(SpecTypeId.Length).GetUnitTypeId();


                var pList= new List<clPileFamilyInfo>();
                var cList = new List<clCircelCADInfo>();

                foreach (FamilySymbol item in Elements)
                {
                    pList.Add(new clPileFamilyInfo
                    {
                        Id = item.Id,
                        Family = item.FamilyName,
                        Type = item.Name
                    });
                }

                PileFamilyList.AddRange(pList);

                Options options = doc.Application.Create.NewGeometryOptions();
                GeometryElement gE1 = LinkCAD.get_Geometry(options);

                if (gE1!=null)
                {
                    foreach (GeometryObject gO1 in gE1)
                    {
                        GeometryInstance gI1 = gO1 as GeometryInstance;
                        if (gI1!=null)
                        {
                            GeometryElement gE2 = gI1.GetInstanceGeometry();
                            if (gE2!=null)
                            {
                                foreach (GeometryObject gO2 in gE2)
                                {
                                    if (gO2 is Arc)
                                    {
                                        var Arc = (Arc)gO2;
                                        if (Arc.IsClosed)
                                        {
                                            var Cir = new clCircelCADInfo();
                                            Cir.Diameter = UnitUtils.ConvertFromInternalUnits(Arc.Radius * 2, DisplayLengthUnitType); ;
                                            Cir.X = UnitUtils.ConvertFromInternalUnits(Arc.Center.X, DisplayLengthUnitType);
                                            Cir.Y = UnitUtils.ConvertFromInternalUnits(Arc.Center.Y, DisplayLengthUnitType);
                                            Cir.Layer = (doc.GetElement(gO2.GraphicsStyleId) as GraphicsStyle).GraphicsStyleCategory.Name;

                                            cList.Add(Cir);
                                        }    
                                    }    
                                }    
                            }    
                        }    
                    }
                }    

                CircleList.AddRange(cList);
            }
            catch
            {

            }

        }

        private ActionCommand cmdVeCoc;

        public ICommand CmdVeCoc
        {
            get
            {
                if (cmdVeCoc == null)
                {
                    cmdVeCoc = new ActionCommand(PerformCmdVeCoc);
                }

                return cmdVeCoc;
            }
        }

        private void PerformCmdVeCoc()
        {
            try
            {
                var doc = LinkCAD.Document;

                var DisplayLengthUnitType = doc.GetUnits().GetFormatOptions(SpecTypeId.Length).GetUnitTypeId();

                var faSym = doc.GetElement(PileFamilySelect.Id) as FamilySymbol;

                if (faSym!=null)
                {
                    using (Transaction tr = new Transaction(doc, "Vẽ cọc"))
                    {
                        tr.Start();

                        if (!faSym.IsActive)
                        {
                            faSym.Activate();
                            doc.Regenerate();
                        }

                        foreach (var c in CircleList)
                        {
                            XYZ loc = new XYZ
                                (UnitUtils.ConvertToInternalUnits(c.X.Value, DisplayLengthUnitType),
                                UnitUtils.ConvertToInternalUnits(c.Y.Value, DisplayLengthUnitType),
                                0);

                            var instance = doc.Create.NewFamilyInstance(loc, faSym, StructuralType.Footing);
                        }


                        tr.Commit();

                        TaskDialog.Show("Thông báo", "Đã vẽ xong");
                    }    

                    
                }    
            }
            catch
            {

            }
        }
    }
}
