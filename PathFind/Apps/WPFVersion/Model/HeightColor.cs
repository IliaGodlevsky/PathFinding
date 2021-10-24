using Common.Extensions;
using GraphLib.Base;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace WPFVersion.Model
{
    internal sealed class HeightColor
    {
        public Dictionary<int, Brush> HeightColors { get; private set; }

        public HeightColor(IGraph graph)
        {
            this.graph = graph;
            previousColors = new List<Brush>();
            FormShadesOfGray();
        }

        public void ColorizeAccordingToCost()
        {
            foreach (Vertex vertex in graph.Vertices)
            {
                previousColors.Add(vertex.Background);
                if (!vertex.IsObstacle)
                {
                    if (!vertex.IsVisualizedAsEndPoint && !vertex.IsVisualizedAsPath)
                    {
                        vertex.Background = HeightColors[vertex.Cost.CurrentCost];
                    }
                }
            }
        }

        public void Reset()
        {
            for (int i = 0; i < previousColors.Count; i++)
            {
                if (graph.Vertices.ElementAt(i) is Vertex vertex)
                {
                    vertex.Background = previousColors[i];
                }
            }
            previousColors.Clear();
        }

        public void FormShadesOfGray()
        {
            if (PreviousAmplitude != CurrentAmplitude)
            {                
                PreviousAmplitude = CurrentAmplitude;
                HeightColors = new Dictionary<int, Brush>();
                values = BaseVertexCost.CostRange.GetAllValues();
                double step = byte.MaxValue / CurrentAmplitude;
                for (int i = 0; i < CurrentAmplitude; i++)
                {
                    var color = Colors.LightSeaGreen;
                    color.A = Convert.ToByte(i * step);
                    var brush = new SolidColorBrush(color);
                    HeightColors.Add(values[i], brush);
                }
            }
        }
       
        private int CurrentAmplitude => BaseVertexCost.CostRange.Amplitude() + 1;
        private int PreviousAmplitude { get; set; }

        private int[] values;
        private List<Brush> previousColors;
        private readonly IGraph graph;
    }
}