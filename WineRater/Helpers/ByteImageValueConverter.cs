using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WineRater.Helpers
{
    public class ByteImageValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is byte[])
            {
                byte[] bytes = value as byte[];
                var stream = new MemoryStream(bytes);
                var image = new BitmapImage();
                image.SetSource(stream);

                return image;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
