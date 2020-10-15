using GraphLibrary.Coordinates;
using GraphLibrary.Coordinates.Interface;
using GraphLibrary.DTO;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ConsoleVersion.Model.Vertex
{
    [Serializable]
    internal class ConsoleVertex : IVertex
    {
        public event EventHandler OnDestinationChosen;
        public event EventHandler OnCostChanged;
        public event EventHandler OnRoleChanged;

        public ConsoleVertex()
        {
            this.Initialize();
        }

        public ConsoleVertex(VertexDto dto) : this()
        {
            this.InitializeBy(dto);
        }

        public bool IsEnd { get; set; }
        public bool IsObstacle { get; set; }
        public bool IsStart { get; set; }
        public bool IsVisited { get; set; }
        public int Cost { get; set; }
        public Color Colour { get; set; }
        public IList<IVertex> Neighbours { get; set; }
        public IVertex ParentVertex { get; set; }
        public double AccumulatedCost { get; set; }
        public ICoordinate Position { get; set; }
        public VertexDto Dto => new VertexDto(this);

        public void ChangeCost()
        {
            OnCostChanged?.Invoke(this, new EventArgs());
        }

        public void ChangeRole()
        {
            OnRoleChanged?.Invoke(this, new EventArgs());
        }

        public void SetAsExtremeVertex()
        {
            OnDestinationChosen?.Invoke(this, new EventArgs());
        }

        public void MarkAsEnd()
        {
            Colour = Color.FromKnownColor(KnownColor.Red);
        }

        public void MarkAsSimpleVertex()
        {
            Colour = Color.FromKnownColor(KnownColor.White);
        }

        public void MarkAsObstacle()
        {
            this.WashVertex();
            Colour = Color.FromKnownColor(KnownColor.Black);
        }

        public void MarkAsPath()
        {
            Colour = Color.FromKnownColor(KnownColor.Yellow);
        }

        public void MarkAsStart()
        {
            Colour = Color.FromKnownColor(KnownColor.Green);
        }

        public void MarkAsVisited()
        {
            Colour = Color.FromKnownColor(KnownColor.Blue);
        }

        public void MarkAsEnqueued()
        {
            Colour = Color.FromKnownColor(KnownColor.Orange);
        }
    }
}
