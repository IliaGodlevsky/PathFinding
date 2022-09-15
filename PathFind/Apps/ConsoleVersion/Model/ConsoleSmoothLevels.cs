using Autofac;
using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using GraphLib.Interfaces;
using GraphLib.Realizations.SmoothLevel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleVersion.Model
{
    internal static class ConsoleSmoothLevels
    {
        private static readonly Lazy<IReadOnlyList<ISmoothLevel>> levels;

        public static IReadOnlyList<ISmoothLevel> Levels => levels.Value;

        static ConsoleSmoothLevels()
        {
            levels = new Lazy<IReadOnlyList<ISmoothLevel>>(GetSmoothLevels().ToReadOnly);
        }

        private sealed class CustomSmoothLevel : ISmoothLevel
        {
            private const int MaxSmoothLevel = 100;
            private const string LevelMsg = "Input level of smoothing: ";

            private IInput<int> IntInput { get; } = DI.Container.Resolve<IInput<int>>();

            public int Level => IntInput.Input(LevelMsg, MaxSmoothLevel, 1);

            public override string ToString() => "Custom";
        }

        private static IEnumerable<ISmoothLevel> GetSmoothLevels()
        {
            return SmoothLevels.Levels.Append(new CustomSmoothLevel());
        }
    }
}
