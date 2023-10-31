using Pathfinding.App.Console.Settings;
using System.Collections.Generic;
using System.Drawing;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal abstract class FramedOrdinate : FramedAxis
    {
        private readonly string VerticalFrameComponent;

        protected readonly int graphLength;
        protected readonly int yCoordinatePadding;

        protected FramedOrdinate(int graphLength)
        {
            VerticalFrameComponent = Parametres.Default.VerticalFrameComponent;
            this.graphLength = graphLength;
            yCoordinatePadding = AppLayout.YCoordinatePadding;
        }

        protected override IEnumerable<Fracture> GetCoordinates()
        {
            int x = 0 + ValueOffset;
            int y = AppLayout.HeightOfAbscissaView + AppLayout.HeightOfGraphParametresView;
            var start = new Point(x, y);
            for (int i = 0; i < graphLength; i++)
            {
                var coord = new Point(start.X, start.Y + i);
                string index = GetPaddedIndex(i);
                yield return new(Value: index, Coordinate: coord);
            }
        }

        protected override IEnumerable<Fracture> GetFrames()
        {
            int y = AppLayout.HeightOfAbscissaView + AppLayout.HeightOfGraphParametresView;
            var start = new Point(yCoordinatePadding + FrameOffset, y);
            for (int i = 0; i < graphLength; i++)
            {
                var coord = new Point(start.X, start.Y + i);
                yield return new (VerticalFrameComponent, coord);
            }
        }

        protected abstract string GetPaddedIndex(int index);
    }
}