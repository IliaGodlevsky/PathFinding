using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using WPFVersion3D.Infrastructure.States;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Converters
{
    internal sealed class RotationStateConverter : IValueConverter
    {
        private IReadOnlyDictionary<object, IRotationState> States { get; }

        public RotationStateConverter()
        {
            States = new Dictionary<object, IRotationState>()
            {
                {true, new EnabledRotationState() },
                {false, new DisabledRotationState() }
            }.ToReadOnly();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return States.GetOrDefault(value, () => NullRotationState.Interface);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
