using System;
using System.Windows.Media;
using ValueRange;

namespace WPFVersion3D.Extensions
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
            var percentValueRange = new InclusiveValueRange<double>(0, 1);
            percentFromMax = percentValueRange.ReturnInRange(percentFromMax / 100);
            var newBrightness = Math.Round(byte.MaxValue * percentFromMax, 0);
            color.A = Convert.ToByte(newBrightness);
            return color;
        }
    }
}
