using System;
using Windows.UI.Xaml.Data;

namespace Zalando.Client.Converter
{
    public class InvertBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var boolValue = value as bool?;

            if (boolValue == null)
            {
                return null;
            }

            return !boolValue.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
