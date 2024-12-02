using Autodesk.AutoCAD.DatabaseServices;
using K008Libs.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADNet.TKC
{
    public class clBlockInfo : PropertyChangedBase
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

        private ObservableRangeCollection<string> _AttList = new ObservableRangeCollection<string>();
        public ObservableRangeCollection<string> AttList
        {
            get
            {
                return _AttList;
            }
            set
            {
                _AttList = value;
                OnPropertyChanged();
            }
        }

    }
}
