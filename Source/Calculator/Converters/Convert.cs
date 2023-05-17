using Microsoft.UI.Xaml;

namespace Calculator.Converters
{
    public static class Convert
    {
        public static Visibility NullToCollapsed(object? value)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }
    }
}