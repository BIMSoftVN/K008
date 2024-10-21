using iTask.Classes;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace iTask.Converter
{
    public class TrangThaiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is TrangThai TT)
                {
                    switch (TT)
                    {
                        case TrangThai.ChuaThucHien:
                            return "Chưa thực hiện";
                        case TrangThai.DangThucHien:
                            return "Đang thực hiện";
                        case TrangThai.DangKiemTra:
                            return "Đang kiểm tra";
                        case TrangThai.HoanThanh:
                            return "Hoàn thành";
                        case TrangThai.TamDung:
                            return "Tạm dừng";
                        case TrangThai.HuyBo:
                            return "HuyBo";
                    }    
                }    
            }
            catch
            {

            }
            return "Không có giá trị";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TrangThaiColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is TrangThai TT)
                {
                    switch (TT)
                    {
                        case TrangThai.ChuaThucHien:
                            return Brushes.Gray;
                        case TrangThai.DangThucHien:
                            return Brushes.Blue;
                        case TrangThai.DangKiemTra:
                            return Brushes.Orange;
                        case TrangThai.HoanThanh:
                            return Brushes.Green; ;
                        case TrangThai.TamDung:
                            return Brushes.Red; ;
                        case TrangThai.HuyBo:
                            return Brushes.LightGray; ;
                    }
                }
            }
            catch
            {

            }
            return "Không có giá trị";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TrangThaiIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is TrangThai TT)
                {
                    switch (TT)
                    {
                        case TrangThai.ChuaThucHien:
                            return PackIconKind.ProgressClock;
                        case TrangThai.DangThucHien:
                            return PackIconKind.PlayCircle;
                        case TrangThai.DangKiemTra:
                            return PackIconKind.DatabaseSearch;
                        case TrangThai.HoanThanh:
                            return PackIconKind.CheckCircle;
                        case TrangThai.TamDung:
                            return PackIconKind.Pause;
                        case TrangThai.HuyBo:
                            return PackIconKind.CloseBoxOutline;
                    }
                }
            }
            catch
            {

            }
            return "Không có giá trị";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
