﻿using GraphLib.Interfaces;
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
        private readonly List<Brush> previousColors;
        private readonly IGraph graph;

        public Color CostColor { get; set; }

        public CostColors(IGraph graph)
        {
            this.graph = graph;
            previousColors = new List<Brush>();
            costColors = new Lazy<IReadOnlyDictionary<int, Brush>>(FormCostColors);
            CostColor = Colors.DodgerBlue;
        }

        public void ColorizeAccordingToCost()
        {
            foreach (Vertex vertex in graph)
            {
                previousColors.Add(vertex.Background);
                if (CanBeColored(vertex))
                {
                    vertex.Background = costColors.Value[vertex.Cost.CurrentCost];
                }
            }
        }

        public void ReturnPreviousColors()
        {
            using (var iterator = graph.GetEnumerator())
            {
                for (int i = 0; i < previousColors.Count; i++)
                {
                    iterator.MoveNext();
                    var vertex = (Vertex)iterator.Current;
                    vertex.Background = previousColors[i];
                }
            }
            previousColors.Clear();
        }

        private IReadOnlyDictionary<int, Brush> FormCostColors()
        {
            var availableCostValues = Enumerable
                .Range(CostRange.LowerValueOfRange, (int)CostRange.Amplitude() + 1)
                .ToArray();
            var colors = new Dictionary<int, Brush>();
            double step = byte.MaxValue / availableCostValues.Length;
            for (int i = 0; i < availableCostValues.Length; i++)
            {
                var color = CostColor;
                color.A = Convert.ToByte(i * step);
                var brush = new SolidColorBrush(color);
                colors.Add(availableCostValues[i], brush);
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