using Pathfinding.App.Console.Settings;
using System.Collections.Generic;
using System.Drawing;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal abstract class FramedAbscissa : FramedAxis
    {
        private readonly string CoordinateDelimiter;
        private readonly char FrameComponent;
        private readonly int graphWidth;

        protected FramedAbscissa(int graphWidth)
        {
            CoordinateDelimiter = Parametres.Default.CoordinateDelimiter;
            FrameComponent = Parametres.Default.FrameComponent;
            this.graphWidth = graphWidth;
        }

        protected override IEnumerable<Fracture> GetCoordinates()
        {
            int x = AppLayout.WidthOfOrdinateView;
            int y = AppLayout.HeightOfGraphParametresView 
                + AppLayout.HeightOfAbscissaView - 2 + ValueOffset;
            var start = new Point(x, y);
            for (int i = 0; i < graphWidth; i++)
            {
                var increment = LateralDistance * i;
                var coordinate = new Point(start.X + increment, start.Y);
                yield return new (Value: i.ToString(), Coordinate: coordinate);
            }
        }

        protected override IEnumerable<Fracture> GetFrames()
        {
            int x = AppLayout.WidthOfOrdinateView;
            int y = AppLayout.HeightOfGraphParametresView 
                + AppLayout.HeightOfAbscissaView - 1 + FrameOffset;
            var start = new Point(x, y);
            var fracture = new string(FrameComponent, LateralDistance - 1);
            var frame = string.Concat(CoordinateDelimiter, fracture);
            for (int i = 0; i < graphWidth; i++)
            {
                var increment = LateralDistance * i;
                var coord = new Point(start.X + increment, start.Y);
                yield return new (Value: frame, Coordinate: coord);
            }
            x = start.X + LateralDistance * graphWidth;
            var coordinate = new Point(x, start.Y);
            yield return new (Value: CoordinateDelimiter, Coordinate: coordinate);
        }
    }
}
