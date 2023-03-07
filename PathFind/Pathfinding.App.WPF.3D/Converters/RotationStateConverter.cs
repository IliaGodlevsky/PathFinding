using Pathfinding.App.WPF._3D.Infrastructure.States;
using Pathfinding.App.WPF._3D.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Pathfinding.App.WPF._3D.Converters
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
            };
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return States.GetOrDefault(value, NullRotationState.Interface);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
