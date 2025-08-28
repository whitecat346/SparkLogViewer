using System.Globalization;
using SparkLogViewer.Core.Enums;

namespace SparkLogViewer.Converters;

public class LogLevelToTextColorConverter : IValueConverter
{
    /// <inheritdoc />
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not LogLevel level)
        {
            return Colors.Transparent;
        }

        return level switch
        {
            LogLevel.Warning => Color.FromArgb("#FFFBE5"),
            LogLevel.Error => Color.FromArgb("#FFF0F0"),
            LogLevel.Fatal => Color.FromArgb("#F5C8C8"),
            LogLevel.Debug => Color.FromArgb("#F3F3F3"),
            _ => Colors.White
        };
    }

    /// <inheritdoc />
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
