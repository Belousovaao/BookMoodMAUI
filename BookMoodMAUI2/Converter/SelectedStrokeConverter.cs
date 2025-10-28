using System.Globalization;


namespace BookMoodMAUI2.Converter
{
    public class SelectedStrokeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (bool)value ? Colors.Red : Colors.Transparent;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
