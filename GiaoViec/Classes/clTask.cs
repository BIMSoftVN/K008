using GiaoViec.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaoViec.Classes
{
    public class clTask : PropertyChangedBase
    {
        private string _Id;
        public string Id 
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

        private string _Ten;
        public string Ten
        {
            get
            {
                return _Ten;
            }
            set
            {
                _Ten = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _NgayTao;
        public DateTime? NgayTao
        {
            get
            {
                return _NgayTao;
            }
            set
            {
                _NgayTao = value;
                OnPropertyChanged();
            }
        }

        private string _MoTa;
        public string MoTa
        {
            get
            {
                return _MoTa;
            }
            set
            {
                _MoTa = value;
                OnPropertyChanged();
            }
        }
    }
}
