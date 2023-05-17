using Microsoft.UI.Xaml.Data;

namespace Calculator.Converters
{
    public class NullableIntConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, string? language)
        {
            if (targetType == typeof(string))
                return value?.ToString();

            return value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, string? language)
        {
            return value is string s && !string.IsNullOrWhiteSpace(s) && int.TryParse(s, out int result) ? result : null;
        }
    }
}