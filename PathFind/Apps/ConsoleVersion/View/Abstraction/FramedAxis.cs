using ConsoleVersion.View.Interface;

namespace ConsoleVersion.View.Abstraction
{
    /// <summary>
    /// An abstract class, that provides methods for
    /// generating framed axis of a graph field
    /// </summary>
    internal abstract class FramedAxis : IFramedAxis
    {
        public abstract string GetFramedAxis();

        protected abstract string Offset { get; }

        protected const char Space = ' ';

        protected int LateralDistance => MainView.GetLateralDistanceBetweenVertices();
    }
}
