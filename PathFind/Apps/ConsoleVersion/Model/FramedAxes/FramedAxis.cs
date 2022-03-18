using ConsoleVersion.Interface;
using ConsoleVersion.Views;

namespace ConsoleVersion.Model.FramedAxes
{
    internal abstract class FramedAxis : IFramedAxis, IDisplayable
    {
        protected static int LateralDistance => MainView.LateralDistanceBetweenVertices;

        protected const char Space = ' ';

        protected abstract string Offset { get; }

        public abstract string GetFramedAxis();

        public abstract void Display();
    }
}