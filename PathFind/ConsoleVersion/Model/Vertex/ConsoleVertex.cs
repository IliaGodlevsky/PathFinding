using GraphLibrary;
using GraphLibrary.Extensions;
using GraphLibrary.Vertex;
using System.Collections.Generic;
using System.Drawing;

namespace ConsoleVersion.Vertex
{
    public class ConsoleVertex : IVertex
    {
        public ConsoleVertex()
        {
            this.Initialize();
        }

        public ConsoleVertex(VertexInfo info) : this()
        {
            this.Initialize(info);
        }

        public bool IsEnd { get; set; }
        public bool IsObstacle { get; set; }

        public bool IsSimpleVertex => !IsStart && !IsEnd;

        public bool IsStart { get; set; }
        public bool IsVisited { get; set; }
        public string Text { get; set; }
        public Color Colour { get; set; }
        public List<IVertex> Neighbours { get; set; }
        public IVertex ParentVertex { get; set; }
        public double Value { get; set; }
        public Point Location { get; set; }

        public VertexInfo Info => new VertexInfo(this);

        public void MarkAsCurrentlyLooked(){ return; }

        public void MarkAsEnd() => Colour = Color.FromKnownColor(KnownColor.Red);

        public void MarkAsSimpleVertex() => Colour = Color.FromKnownColor(KnownColor.White);

        public void MarkAsObstacle()
        {
            Text = " ";
            IsObstacle = true;
        }

        public void MarkAsPath() => Colour = Color.FromKnownColor(KnownColor.Yellow);

        public void MarkAsStart() => Colour = Color.FromKnownColor(KnownColor.Green);

        public void MarkAsVisited() => Colour = Color.FromKnownColor(KnownColor.Blue);
    }
}
