using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Settings;
using Shared.Extensions;
using Shared.Primitives.Attributes;
using Shared.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Pathfinding.App.Console.Model.Notes
{
    internal sealed class Statistics
    {
        public static readonly Statistics Empty = new();

        private static readonly IReadOnlyList<string> names;

        static Statistics()
        {
            names = typeof(Statistics)
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(DisplayableAttribute)))
                .OrderByOrderAttribute()
                .Select(prop => prop.GetAttributeOrDefault<DisplayNameSourceAttribute>()?.ResourceName ?? prop.Name)
                .Select(GetString)
                .Skip(1).ToList().AsReadOnly();
        }

        [NonDisplayable]
        public Algorithms Algorithm { get; set; }

        [NonDisplayable]
        public AlgorithmResultStatus ResultStatus { get; set; }

        [NonDisplayable]
        public Heuristics? Heuristics { get; set; }

        [NonDisplayable]
        public StepRules? StepRule { get; set; }

        [NonDisplayable]
        public TimeSpan? Elapsed { get; set; } = default;

        [Order(1)]
        [Displayable]
        [DisplayNameSource(nameof(Languages.Name))]
        public string Name => GetString(Algorithm.ToString());

        [Order(9)]
        [Displayable]
        [DisplayNameSource(nameof(Languages.Status))]
        public string Status => GetString(ResultStatus.ToString());

        [Order(2)]
        [Displayable]
        [DisplayNameSource(nameof(Languages.Time))]
        public string Time => Elapsed?.ToString(Parametres.Default.TimeFormat, 
            CultureInfo.InvariantCulture);

        [Order(3)]
        [Displayable]
        [DisplayNameSource(nameof(Languages.Visited))]
        public int? Visited { get; set; } = default;

        [Order(4)]
        [Displayable]
        [DisplayNameSource(nameof(Languages.Steps))]
        public int? Steps { get; set; } = default;

        [Order(5)]
        [Displayable]
        [DisplayNameSource(nameof(Languages.Cost))]
        public double? Cost { get; set; } = default;

        [Order(6)]
        [Displayable]
        [DisplayNameSource(nameof(Languages.Rule))]
        public string Rule => GetString(StepRule.ToString());

        [Order(7)]
        [Displayable]
        [DisplayNameSource(nameof(Languages.Heuristics))]
        public string Heuristic => GetString(Heuristics.ToString());

        [Order(8)]
        [Displayable]
        [DisplayNameSource(nameof(Languages.Spread))]
        public int? Spread { get; set; } = default;

        private static string GetString(string key)
        {
            return Languages.ResourceManager.GetString(key) ?? key;
        }

        public override string ToString()
        {
            var values = new object[] { Time, Visited, 
                Steps, Cost, Rule, Heuristic, Spread, Status };
            var builder = new StringBuilder($" {Name}  ");
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != null && !values[i].Equals(string.Empty))
                {
                    string value = values[i].ToString();
                    builder.Append($"{names[i]}: {value}  ");
                }
            }
            return builder.ToString();
        }
    }
}
