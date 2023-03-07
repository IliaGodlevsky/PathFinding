using Pathfinding.GraphLib.Smoothing.Interface;
using Pathfinding.GraphLib.Smoothing.Realizations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class WpfSmoothLevels
    {
        private static readonly Lazy<IReadOnlyCollection<ISmoothLevel>> levels;

        public static IEnumerable<ISmoothLevel> Levels => levels.Value;

        static WpfSmoothLevels()
        {
            levels = new Lazy<IReadOnlyCollection<ISmoothLevel>>(GetLevels);
        }

        public sealed class CustomSmoothLevel : ISmoothLevel
        {
            public int Level { get; set; }

            public CustomSmoothLevel(int level)
            {
                Level = level;
            }

            public override string ToString() => "Custom";
        }

        private static IReadOnlyCollection<ISmoothLevel> GetLevels()
        {
            return SmoothLevels.Levels.Append(new CustomSmoothLevel(1)).ToArray();
        }
    }
}
