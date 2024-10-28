using K008Libs.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace iTask.Classes
{
    public class clTTChart : PropertyChangedBase
    {
        private string _TrangThai;
        public string TrangThai
        {
            get
            {
                return _TrangThai;
            }
            set
            {
                _TrangThai = value;
                OnPropertyChanged();
            }
        }

        private SolidColorBrush _Color;
        public SolidColorBrush Color
        {
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;
                OnPropertyChanged();
            }
        }

        private long? _GiaTri;
        public long? GiaTri
        {
            get
            {
                return _GiaTri;
            }
            set
            {
                _GiaTri = value;
                OnPropertyChanged();
            }
        }

    }
}
