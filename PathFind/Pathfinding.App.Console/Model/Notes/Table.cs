using Pathfinding.App.Console.Localization;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pathfinding.App.Console.Model.Notes
{
    internal sealed class Table<T>
    {
        private readonly IReadOnlyCollection<T> values;
        private readonly IReadOnlyList<PropertyInfo> properties;
        private readonly Lazy<IReadOnlyDictionary<string, int>> widths;
        private readonly Lazy<IReadOnlyList<string>> headers;
        private readonly Lazy<IReadOnlyList<IReadOnlyList<string>>> rows;
        private readonly int additionalPadding = 2;

        private IReadOnlyDictionary<string, int> Widths => widths.Value;

        public IReadOnlyList<string> Headers => headers.Value;

        public IReadOnlyList<IReadOnlyList<string>> Rows => rows.Value;

        public Table(IEnumerable<T> values)
        {
            rows = new(GetRows);
            headers = new(GetHeaders);
            widths = new(CalculateWidths);
            properties = typeof(T)
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(DisplayableAttribute)))
                .OrderByOrderAttribute().ToList().AsReadOnly();
            this.values = values.ToReadOnly();
        }

        private IReadOnlyList<string> GetHeaders()
        {
            var headers = new List<string>();
            foreach (var property in properties)
            {
                string name = GetName(property);
                var width = Widths[name] + additionalPadding;
                string header = name.PadRight(width);
                headers.Add(header);
            }
            return headers.AsReadOnly();
        }

        private IReadOnlyList<IReadOnlyList<string>> GetRows()
        {
            var rows = new List<IReadOnlyList<string>>();
            foreach (var item in values)
            {
                var row = new List<string>();
                foreach (var property in properties)
                {
                    string name = GetName(property);
                    object value = property.GetValue(item);
                    string str = value?.ToString() ?? string.Empty;
                    int padding = Widths[name] + additionalPadding;
                    string padded = str.PadRight(padding);
                    row.Add(padded);
                }
                rows.Add(row.AsReadOnly());
            }
            return rows.AsReadOnly();
        }

        private IReadOnlyDictionary<string, int> CalculateWidths()
        {
            var widths = new Dictionary<string, int>();
            foreach (var property in properties)
            {
                string name = GetName(property);
                int maxLength = name.Length;
                foreach (var item in values)
                {
                    object value = property.GetValue(item);
                    if (value != null)
                    {
                        int length = value.ToString().Length;
                        maxLength = Math.Max(maxLength, length);
                    }
                }
                widths[name] = maxLength;
            }
            return widths.AsReadOnly();
        }

        private static string GetName(PropertyInfo prop)
        {
            var attribute = prop.GetAttributeOrDefault<DisplayNameSourceAttribute>();
            return Languages.ResourceManager.GetString(attribute?.ResourceName ?? string.Empty) ?? attribute?.ResourceName ?? prop.Name;
        }
    }
}