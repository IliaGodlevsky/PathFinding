using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfVersion.Converters
{
    public class PathFindResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            string oldLine = (string)value;
            string newString = string.Empty;
            for (int i = 0; i < oldLine.Length; i++)
            {
                if (oldLine[i] != '\n')
                    newString += oldLine[i];
                else
                    newString += " | ";
            }
            return newString;
        }

        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
