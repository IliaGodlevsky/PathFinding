using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GraphLib.Realizations.SmoothLevel
{
    public static class SmoothLevels
    {
        private static readonly Lazy<IReadOnlyList<ISmoothLevel>> levels;

        public static IReadOnlyList<ISmoothLevel> Levels => levels.Value;

        static SmoothLevels()
        {
            levels = new Lazy<IReadOnlyList<ISmoothLevel>>(GetSmoothLevels);
        }

        private sealed class FlatSmoothLevel : ISmoothLevel
        {
            public int Level => 25;

            public override string ToString() => "Flat";
        }

        private sealed class HighSmoothLevel : ISmoothLevel
        {
            public int Level => 3;

            public override string ToString() => "High";
        }

        private sealed class MediumSmoothLevel : ISmoothLevel
        {
            public int Level => 2;

            public override string ToString() => "Medium";
        }

        private sealed class LowSmoothLevel : ISmoothLevel
        {
            public int Level => 1;

            public override string ToString() => "Low";
        }

        private static IReadOnlyList<ISmoothLevel> GetSmoothLevels()
        {
            var smoothLevels = new ISmoothLevel[]
            {
                new LowSmoothLevel(),
                new MediumSmoothLevel(),
                new HighSmoothLevel(),
                new FlatSmoothLevel()
            };
            return new ReadOnlyCollection<ISmoothLevel>(smoothLevels);
        }
    }
}
