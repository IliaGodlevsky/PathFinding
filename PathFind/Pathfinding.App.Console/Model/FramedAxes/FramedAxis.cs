using Pathfinding.App.Console.Interface;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal abstract class FramedAxis : IFramedAxis, IDisplayable
    {
        protected static int LateralDistance => Screen.LateralDistanceBetweenVertices;

        protected const char Space = ' ';

        protected abstract string Offset { get; }

        public abstract string GetFramedAxis();

        public abstract void Display();
    }
}