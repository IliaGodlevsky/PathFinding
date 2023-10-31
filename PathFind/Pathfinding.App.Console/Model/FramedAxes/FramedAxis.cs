using Pathfinding.App.Console.Interface;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal abstract class FramedAxis : IDisplayable
    {
        protected readonly record struct Fracture(string Value, Point Coordinate);

        protected static int LateralDistance => AppLayout.LateralDistanceBetweenVertices;

        protected abstract int ValueOffset { get; }

        protected abstract int FrameOffset { get; }

        public void Display()
        {
            var values = GetCoordinates()
                .Concat(GetFrames()).ToArray();
            foreach (var value in values)
            {
                Cursor.SetPosition(value.Coordinate);
                Terminal.Write(value.Value);
            }
        }

        protected abstract IEnumerable<Fracture> GetCoordinates();

        protected abstract IEnumerable<Fracture> GetFrames();
    }
}