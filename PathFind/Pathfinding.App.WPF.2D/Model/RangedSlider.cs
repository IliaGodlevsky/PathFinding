using Shared.Primitives.ValueRange;
using System.Windows.Controls;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class RangedSlider : Slider
    {
        private InclusiveValueRange<double> valueRange;
        public InclusiveValueRange<double> ValueRange
        {
            get => valueRange;
            set
            {
                valueRange = value;
                Minimum = valueRange.LowerValueOfRange;
                Maximum = valueRange.UpperValueOfRange;
            }
        }

        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            var upperValueRange = valueRange.UpperValueOfRange;
            valueRange = new InclusiveValueRange<double>(newMinimum, upperValueRange);
            base.OnMinimumChanged(oldMinimum, newMinimum);
        }

        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            var lowerValueRange = valueRange.LowerValueOfRange;
            valueRange = new InclusiveValueRange<double>(lowerValueRange, newMaximum);
            base.OnMaximumChanged(oldMaximum, newMaximum);
        }
    }
}
