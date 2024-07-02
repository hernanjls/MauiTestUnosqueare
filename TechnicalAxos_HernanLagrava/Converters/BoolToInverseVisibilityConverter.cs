
using System.Globalization;

namespace TechnicalAxos_HernanLagrava.Converters
{
    public class BoolToInverseVisibilityConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            value is bool boolValue ? !boolValue : throw new InvalidOperationException("The target must be a boolean");

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            throw new NotSupportedException("ConvertBack is not supported");
    }
}
