﻿using Common.ValueRanges;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFVersion.Converters
{
    internal class StringToRangedDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round((double)value, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0D;

            if (parameter as ValueRange != null && value != null)
            {
                if (double.TryParse(value.ToString(), out result))
                {
                    var sliderValue = System.Convert.ToInt32(result);
                    if ((parameter as ValueRange).IsInBounds(sliderValue))
                    {
                        result = sliderValue;
                    }
                    else
                    {
                        result = (parameter as ValueRange).UpperRange;
                    }
                }
            }

            return result;
        }
    }
}
