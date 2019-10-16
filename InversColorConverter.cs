using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ColorsHelper
{
    public class InversColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush b = value as SolidColorBrush;
            if (b == null)
                return null;
            Color c = b.Color;
            return new SolidColorBrush(Color.FromScRgb(1-c.ScA, 255-c.ScR, 255-c.ScG, 255-c.ScB));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
