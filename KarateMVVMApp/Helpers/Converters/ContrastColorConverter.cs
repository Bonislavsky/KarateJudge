using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace KarateMVVMApp.Helpers.Converters
{
    public class ContrastColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string colorString && !string.IsNullOrEmpty(colorString))
            {
                if (Color.TryParse(colorString, out Color color))
                {
                    var brightness = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B);

                    return brightness > 160 ? Brushes.Black : Brushes.White;
                }
            }

            return Brushes.White;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}