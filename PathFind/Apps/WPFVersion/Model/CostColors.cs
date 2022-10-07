using Common.Disposables;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using ValueRange.Extensions;
using static GraphLib.Base.BaseVertexCost;

namespace WPFVersion.Model
{
    internal sealed class CostColors
    {
        private readonly Lazy<IReadOnlyDictionary<int, Brush>> costColors;
        private readonly ICollection<Brush> previousColors;
        private readonly IReadOnlyCollection<Vertex> graph;

        public Color CostColor { get; set; } = Colors.DodgerBlue;

        public CostColors(IGraph graph)
        {
            this.graph = graph.Cast<Vertex>().ToReadOnly();
            previousColors = new Collection<Brush>();
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
            var availableCostValues = Enumerable
                .Range(CostRange.LowerValueOfRange, (int)CostRange.Amplitude() + 1)
                .ToReadOnly();
            var colors = new Dictionary<int, Brush>();
            double step = byte.MaxValue / availableCostValues.Count;
            for (int i = 0; i < availableCostValues.Count; i++)
            {
                var color = CostColor;
                color.A = Convert.ToByte(i * step);
                colors.Add(availableCostValues[i], new SolidColorBrush(color));
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