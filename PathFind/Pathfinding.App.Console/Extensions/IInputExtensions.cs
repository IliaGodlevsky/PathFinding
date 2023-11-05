using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IInputExtensions
    {
        public static T Input<T>(this IInput<T> self, string msg)
        {
            Terminal.Write(msg);
            return self.Input();
        }

        public static T Input<T>(this IInput<T> self, string msg, T upper, T lower = default)
            where T : IComparable<T>
        {
            return self.Input(msg, new(upper, lower));
        }

        public static T Input<T>(this IInput<T> self, string msg, InclusiveValueRange<T> range)
            where T : IComparable<T>
        {
            var input = self.Input(msg);
            while (!range.Contains(input))
            {
                input = self.Input(Languages.OutOfRangeMsg);
            }
            return input;
        }
    }
}