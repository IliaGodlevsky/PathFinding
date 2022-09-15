using Common.Extensions.EnumerableExtensions;
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
            levels = new Lazy<IReadOnlyList<ISmoothLevel>>(GetSmoothLevels().ToReadOnly);
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

        private static IEnumerable<ISmoothLevel> GetSmoothLevels()
        {
            yield return new SmoothLevel(1, "Low");
            yield return new SmoothLevel(2, "Medium");
            yield return new SmoothLevel(3, "High");
            yield return new SmoothLevel(25, "Flat");
        }
    }
}
