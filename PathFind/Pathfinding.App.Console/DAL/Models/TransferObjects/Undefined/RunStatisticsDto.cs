#nullable enable
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.App.Console.Settings;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Undefined
{
    internal record RunStatisticsDto
    {
        private static readonly IReadOnlyList<string> names;

        static RunStatisticsDto()
        {
            names = typeof(RunStatisticsDto)
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(DisplayableAttribute)))
                .OrderByOrderAttribute()
                .Select(prop => prop.GetAttributeOrDefault<DisplayNameSourceAttribute>()?.ResourceName ?? prop.Name)
                .Select(name => GetString(name))
                .Skip(1).ToList().AsReadOnly();
        }

        public int AlgorithmRunId { get; set; }

        public string AlgorithmId { get; set; } = null!;

        public string? Heuristics { get; set; } = null;

        public string? StepRule { get; set; } = null;

        [Displayable(1)]
        [DisplayNameSource(nameof(Languages.Name))]
        public string Name => GetString(AlgorithmId);

        [Displayable(10)]
        [DisplayNameSource(nameof(Languages.Status))]
        public string Status => GetString(ResultStatus);

        [Displayable(2)]
        [DisplayNameSource(nameof(Languages.Time))]
        public string? Time => Elapsed?.ToString(Parametres.Default.TimeFormat,
            CultureInfo.InvariantCulture);

        [Displayable(6)]
        [DisplayNameSource(nameof(Languages.Rule))]
        public string? Rule => GetString(StepRule);

        [Displayable(7)]
        [DisplayNameSource(nameof(Languages.Heuristics))]
        public string? Heuristic => GetString(Heuristics);

        [Displayable(3)]
        [DisplayNameSource(nameof(Languages.Visited))]
        public int? Visited { get; set; } = null;

        public string ResultStatus { get; set; } = string.Empty;

        [Displayable(9)]
        public string? Speed => AlgorithmSpeed?.ToString(Parametres.Default.TimeFormat,
            CultureInfo.InvariantCulture);

        public TimeSpan? AlgorithmSpeed { get; set; } = null!;

        public TimeSpan? Elapsed { get; set; } = null;

        [Displayable(4)]
        [DisplayNameSource(nameof(Languages.Steps))]
        public int? Steps { get; set; } = null;

        [Displayable(5)]
        [DisplayNameSource(nameof(Languages.Cost))]
        public double? Cost { get; set; } = null;

        [Displayable(8)]
        [DisplayNameSource(nameof(Languages.Spread))]
        public int? Spread { get; set; } = null;

        public override string ToString()
        {
            var values = new object?[] { Time, Visited, Steps,
                Cost, Rule, Heuristic, Spread, Speed, Status };
            return names.Zip(values, (n, v) => (Name: n, Value: v))
                .Where(x => x.Value is not null and not "")
                .Select(x => $"{x.Name}: {x.Value}")
                .Prepend(Name)
                .To(lines => string.Join("; ", lines));
        }

        private static string GetString(string? key)
        {
            return Languages.ResourceManager.GetString(key ?? string.Empty) ?? key ?? string.Empty;
        }
    }
}
#nullable disable