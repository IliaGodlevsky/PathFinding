using GraphLib.Interfaces;
using System;
using System.Collections.Generic;

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

        private sealed class SmoothLevel : ISmoothLevel
        {
            private readonly string name;

            public int Level { get; }

            public SmoothLevel(int level, string name)
            {
                Level = level;
                this.name = name;
            }

            public override string ToString() => name;
        }

        private static IReadOnlyList<ISmoothLevel> GetSmoothLevels()
        {
            return new ISmoothLevel[]
            {
                new SmoothLevel(1, "Low"),
                new SmoothLevel(2, "Medium"),
                new SmoothLevel(3, "High"),
                new SmoothLevel(25, "Flat")
            };
        }
    }
}
