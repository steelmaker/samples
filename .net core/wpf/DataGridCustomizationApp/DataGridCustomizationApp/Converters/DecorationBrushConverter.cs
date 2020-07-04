using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using DataGridCustomizationApp.ViewModels;

namespace DataGridCustomizationApp.Converters
{
    public class DecorationBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var decorationType = (value as IDecorationItem)?.ItemDecorationType;
            if (decorationType == null) return null;

            if (decorationType == ItemDecorationType.C)
                return Brushes.Chocolate;
            if (decorationType == ItemDecorationType.B)
                return Brushes.LightGreen;
            if (decorationType == ItemDecorationType.A)
                return Brushes.DarkGray;

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
