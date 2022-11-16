using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Windows.Media;

namespace Pathfinding.App.WPF._2D.Extensions
{
    internal static class ColorExtensions
    {
        public static Color SetBrightness(this Color color, double percentFromMax)
        {
            var percentValueRange = new InclusiveValueRange<double>(0, 100);
            percentFromMax = percentValueRange.ReturnInRange(percentFromMax);
            int newBrightness = (byte)(byte.MaxValue * percentFromMax / 100);
            color.A = Convert.ToByte(newBrightness);
            return color;
        }
    }
}
