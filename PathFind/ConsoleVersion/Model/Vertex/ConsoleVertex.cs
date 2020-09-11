using GraphLibrary.DTO;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;
using System.Drawing;

namespace ConsoleVersion.Model.Vertex
{
    internal class ConsoleVertex : IVertex
    {
        public ConsoleVertex()
        {
            this.Initialize();
        }

        public ConsoleVertex(VertexDto info) : this()
        {
            this.Initialize(info);
        }

        public bool IsEnd { get; set; }
        public bool IsObstacle { get; set; }
        public bool IsStart { get; set; }
        public bool IsVisited { get; set; }
        public int Cost { get; set; }
        public Color Colour { get; set; }
        public List<IVertex> Neighbours { get; set; }
        public IVertex ParentVertex { get; set; }
        public double AccumulatedCost { get; set; }
        public Point Location { get; set; }

        public VertexDto Dto => new VertexDto(this);

        public void MarkAsEnd() => Colour = Color.FromKnownColor(KnownColor.Red);

        public void MarkAsSimpleVertex() => Colour = Color.FromKnownColor(KnownColor.White);

        public void MarkAsObstacle()
        {
            this.WashVertex();
            Colour = Color.FromKnownColor(KnownColor.Black);
        }

        public void MarkAsPath() => Colour = Color.FromKnownColor(KnownColor.Yellow);

        public void MarkAsStart() => Colour = Color.FromKnownColor(KnownColor.Green);

        public void MarkAsVisited() => Colour = Color.FromKnownColor(KnownColor.Blue);
    }
}
