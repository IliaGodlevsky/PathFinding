using GraphLib.Interfaces;
using GraphLib.Realizations.SmoothLevel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WPFVersion.Model
{
    internal sealed class WpfSmoothLevels
    {
        private static readonly Lazy<IReadOnlyCollection<ISmoothLevel>> levels;

        public static IReadOnlyCollection<ISmoothLevel> Levels => levels.Value;

        static WpfSmoothLevels()
        {
            levels = new Lazy<IReadOnlyCollection<ISmoothLevel>>(GetLevels);
        }

        public sealed class CustomSmoothLevel : ISmoothLevel
        {
            public int Level { get; set; }

            public override string ToString() => "Custom";
        }

        private static IReadOnlyCollection<ISmoothLevel> GetLevels()
        {
            return SmoothLevels.Levels.Append(new CustomSmoothLevel { Level = 1 }).ToArray();
        }
    }
}
