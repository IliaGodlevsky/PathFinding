using ConsoleVersion.Interface;
using ConsoleVersion.Views;

namespace ConsoleVersion.Model.FramedAxes
{
    internal abstract class FramedAxis : IFramedAxis, IDisplayable
    {
        public abstract string GetFramedAxis();

        public abstract void Display();

        protected abstract string Offset { get; }

        protected const char Space = ' ';

        protected int LateralDistance => MainView.LateralDistanceBetweenVertices;
    }
}
