using K008Libs.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTask.Classes
{
    public enum TrangThai
    {
        ChuaThucHien,
        DangThucHien,
        DangKiemTra,
        HoanThanh,
        TamDung,
        HuyBo
    }

    [Table("Tasks")]
    public class clTask : PropertyChangedBase
    {
        private long _Id= 0;
        public long Id
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


        private DateTime? _Deadline;
        public DateTime? Deadline
        {
            get
            {
                return _Deadline;
            }
            set
            {
                _Deadline = value;
                OnPropertyChanged();
            }
        }


        private TrangThai _TrangThai = TrangThai.ChuaThucHien;
        public TrangThai TrangThai
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



        private string _NguoiGiaoId;
        public string NguoiGiaoId
        {
            get
            {
                return _NguoiGiaoId;
            }
            set
            {
                _NguoiGiaoId = value;
                OnPropertyChanged();
            }
        }

        private string _NguoiNhanId;
        public string NguoiNhanId
        {
            get
            {
                return _NguoiNhanId;
            }
            set
            {
                _NguoiNhanId = value;
                OnPropertyChanged();
            }
        }


        private clUser _NguoiGiao = new clUser();
        public virtual clUser NguoiGiao
        {
            get
            {
                return _NguoiGiao;
            }
            set
            {
                _NguoiGiao = value;
                if (value !=null)
                {
                    NguoiGiaoId = value.Id;
                }    
                
                OnPropertyChanged();
            }
        }


        private clUser _NguoiNhan = new clUser();
        public virtual clUser NguoiNhan
        {
            get
            {
                return _NguoiNhan;
            }
            set
            {
                _NguoiNhan = value;
                if (value != null)
                {
                    NguoiNhanId = value.Id;
                }
                OnPropertyChanged();
            }
        }

    }
}
