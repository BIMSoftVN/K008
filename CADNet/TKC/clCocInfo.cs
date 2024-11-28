using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using K008Libs.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADNet.TKC
{
    public class clCocInfo : PropertyChangedBase
    {
        private ObjectId _Id = new ObjectId();
        public ObjectId Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
                OnPropertyChanged();
            }
        }

        private double? _Diameter = null;
        public double? Diameter
        {
            get
            {
                return _Diameter;
            }
            set
            {
                _Diameter = value;
                OnPropertyChanged();
            }
        }

        private string _Layer = null;
        public string Layer
        {
            get
            {
                return _Layer;
            }
            set
            {
                _Layer = value;
                OnPropertyChanged();
            }
        }

        private Point3d _Orgin = new Point3d();
        public Point3d Orgin
        {
            get
            {
                return _Orgin;
            }
            set
            {
                _Orgin = value;
                OnPropertyChanged();
            }
        }


        private string _Name = null;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged();
            }
        }

        private string _TextLayer = null;
        public string TextLayer
        {
            get
            {
                return _TextLayer;
            }
            set
            {
                _TextLayer = value;
                OnPropertyChanged();
            }
        }

        private double? _DisTance = null;
        public double? DisTance
        {
            get
            {
                return _DisTance;
            }
            set
            {
                _DisTance = value;
                OnPropertyChanged();
            }
        }

    }
}
