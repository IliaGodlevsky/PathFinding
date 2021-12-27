using Common.Extensions;
using System;
using System.Windows.Media;
using ValueRange;
using ValueRange.Extensions;

namespace WPFVersion.Extensions
{
    internal static class ColorExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="percentFromMax">Amount of percents, from 0 to 100</param>
        /// <returns></returns>
        public static Color SetBrightness(this Color color, double percentFromMax)
        {
            var percentValueRange = new InclusiveValueRange<double>(0, 100);
            percentFromMax = percentValueRange.ReturnInRange(percentFromMax);
            int newBrightness = ((int)byte.MaxValue).GetPercentage(percentFromMax);
            color.A = Convert.ToByte(newBrightness);
            return color;
        }
    }
}
