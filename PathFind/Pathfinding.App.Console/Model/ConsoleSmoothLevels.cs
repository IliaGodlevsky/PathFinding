using Autofac;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.GraphLib.Smoothing.Interface;
using Pathfinding.GraphLib.Smoothing.Realizations;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Model
{
    internal static class ConsoleSmoothLevels
    {
        private static readonly Lazy<IReadOnlyList<ISmoothLevel>> levels;

        public static IReadOnlyList<ISmoothLevel> Levels => levels.Value;

        static ConsoleSmoothLevels()
        {
            levels = new(GetSmoothLevels);
        }

        private sealed class CustomSmoothLevel : ISmoothLevel
        {
            private const int MaxSmoothLevel = 100;

            private IInput<int> IntInput { get; } = Registry.Container.Resolve<IInput<int>>();

            public int Level => IntInput.Input(Languages.InputSmoothLevelMsg, MaxSmoothLevel, 1);

            public override string ToString() => Languages.CustomSmoothLevel;
        }

        private static IReadOnlyList<ISmoothLevel> GetSmoothLevels()
        {
            return SmoothLevels.Levels.Append(new CustomSmoothLevel()).ToReadOnly();
        }
    }
}
