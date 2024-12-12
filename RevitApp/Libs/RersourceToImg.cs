using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RevitApp.Libs
{
    internal class RersourceToImg
    {
        public static BitmapFrame ConvertWithHeight(string ImgPath, double Width, double Height)
        {
            BitmapFrame res = null;

            try
            {
                using (Stream resStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ImgPath))
                {
                    BitmapDecoder decoder = BitmapDecoder.Create(resStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    BitmapFrame orgFrame = decoder.Frames[0];
                    res = BitmapFrame.Create(new TransformedBitmap(orgFrame, new ScaleTransform(Width / orgFrame.Width, Height / orgFrame.Height)));
                }
            }
            catch
            {

            }

            return res;
        }
    }
}
