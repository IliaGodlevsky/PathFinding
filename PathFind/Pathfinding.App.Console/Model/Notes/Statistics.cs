#nullable enable
using Autofac;
using Newtonsoft.Json;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Settings;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal record IsExternalInit;
}

namespace Pathfinding.App.Console.Model.Notes
{
    internal sealed record class Statistics(string algorithm)
    {
        private const string Missing = "**********";

        public static readonly Statistics Empty = new(string.Empty);

        private static readonly IReadOnlyList<string> names;

        static Statistics()
        {
            names = typeof(Statistics)
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(DisplayableAttribute)))
                .OrderByOrderAttribute()
                .Select(prop => prop.GetAttributeOrDefault<DisplayNameSourceAttribute>()?.ResourceName ?? prop.Name)
                .Select(name => GetString(name) ?? Missing)
                .Skip(1).ToList().AsReadOnly();
        }

        public string Algorithm { get; } = algorithm!;

        public string? Heuristics { get; init; } = null;

        public string? StepRule { get; init; } = null;

        [Displayable(1)]
        [DisplayNameSource(nameof(Languages.Name))]
        [JsonIgnore]
        public string Name => GetString(Algorithm) ?? Missing;

        [Displayable(10)]
        [DisplayNameSource(nameof(Languages.Status))]
        [JsonIgnore]
        public string Status => GetString(ResultStatus) ?? Missing;

        [Displayable(2)]
        [DisplayNameSource(nameof(Languages.Time))]
        [JsonIgnore]
        public string? Time => Elapsed?.ToString(Parametres.Default.TimeFormat,
            CultureInfo.InvariantCulture);

        [Displayable(6)]
        [DisplayNameSource(nameof(Languages.Rule))]
        [JsonIgnore]
        public string? Rule => GetString(StepRule);

        [Displayable(7)]
        [DisplayNameSource(nameof(Languages.Heuristics))]
        [JsonIgnore]
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

        private static string? GetString(string? key)
        {
            return Languages.ResourceManager.GetString(key ?? string.Empty) ?? key;
        }
    }
}
#nullable disable
