using Common.Disposables;
using Common.Extensions.EnumerableExtensions;
using Common.ReadOnly;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using ValueRange.Extensions;
using static GraphLib.Base.BaseVertexCost;

namespace WPFVersion.Model
{
    internal sealed class CostColors
    {
        private readonly Lazy<IReadOnlyDictionary<int, Brush>> costColors;
        private readonly List<Brush> previousColors;
        private readonly IReadOnlyCollection<Vertex> graph;

        public Color CostColor { get; set; } = Colors.DodgerBlue;

        public CostColors(IGraph graph)
        {
            this.graph = graph.Cast<Vertex>().ToReadOnly();
            previousColors = new List<Brush>();
            costColors = new Lazy<IReadOnlyDictionary<int, Brush>>(FormCostColors);
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
            var costValues = CostRange.EnumerateValues().ToReadOnly();
            var colors = new Dictionary<int, Brush>();
            double step = byte.MaxValue / costValues.Count;
            for (int i = 0; i < costValues.Count; i++)
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
                && !vertex.IsVisualizedAsEndPoint
                && !vertex.IsVisualizedAsPath;
        }
    }
}