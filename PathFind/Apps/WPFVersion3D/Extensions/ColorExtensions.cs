using System;
using System.Windows.Media;
using ValueRange;
using ValueRange.Extensions;

namespace WPFVersion3D.Extensions
{
    internal static class ColorExtensions
    {
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
