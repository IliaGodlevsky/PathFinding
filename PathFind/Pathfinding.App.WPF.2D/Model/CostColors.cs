using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using Shared.Primitives;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class CostColors
    {
        private readonly Lazy<IReadOnlyDictionary<int, Brush>> costColors;
        private readonly List<Brush> previousColors;
        private readonly IReadOnlyCollection<Vertex> graph;

        public Color CostColor { get; set; } = Colors.DodgerBlue;

        public InclusiveValueRange<int> CostRange { get; set; }

        public CostColors(IGraph<Vertex> graph)
        {
            this.graph = graph.ToArray();
            this.previousColors = new();
            this.costColors = new(FormCostColors);
        }

        public void ColorizeAccordingToCost()
        {
            graph.ForEach(vertex => previousColors.Add(vertex.Background))
                .Where(CanBeColored)
                .ForEach(vertex => vertex.Background = costColors.Value[vertex.Cost.CurrentCost]);
        }

        public void ReturnPreviousColors()
        {
            using (Disposable.Use(previousColors.Clear))
            {
                graph.Zip(previousColors, (vertex, color) => (Vertex: vertex, Color: color))
                  .ForEach(item => item.Vertex.Background = item.Color);
            }
        }

        private IReadOnlyDictionary<int, Brush> FormCostColors()
        {
            var costValues = CostRange.Enumerate().ToArray();
            var colors = new Dictionary<int, Brush>();
            double step = byte.MaxValue / costValues.Length;
            for (int i = 0; i < costValues.Length; i++)
            {
                var color = CostColor;
                color.A = Convert.ToByte(i * step);
                var brush = new SolidColorBrush(color);
                colors.Add(costValues[i], brush);
            }
            return new ReadOnlyDictionary<int, Brush>(colors);
        }

        private bool CanBeColored(Vertex vertex)
        {
            return !vertex.IsObstacle
                && !vertex.IsVisualizedAsPath()
                && !vertex.IsVisualizedAsRange();
        }
    }
}