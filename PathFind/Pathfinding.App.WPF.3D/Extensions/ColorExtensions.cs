using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Windows.Media;

namespace Pathfinding.App.WPF._3D.Extensions
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
