using Pathfinding.App.Console.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal abstract class FramedAxis : IDisplayable, IEnumerable<(string Value, Point Coordinate)>
    {
        protected static int LateralDistance 
            => AppLayout.LateralDistanceBetweenVertices;

        protected abstract int ValueOffset { get; }

        protected abstract int FrameOffset { get; }

        public void Display()
        {
            using (var enumerator = GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Display(enumerator.Current);
                }
            }
        }

        protected abstract IEnumerable<(string Value, Point Coordinate)> GetCoordinates();

        protected abstract IEnumerable<(string Value, Point Coordinate)> GetFrames();

        private static void Display((string Value, Point Coordinate) fracture)
        {
            Cursor.SetPosition(fracture.Coordinate);
            Terminal.Write(fracture.Value);
        }

        public IEnumerator<(string Value, Point Coordinate)> GetEnumerator()
        {
            return GetCoordinates().Concat(GetFrames()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}