namespace ConsoleVersion.View.Abstraction
{
    internal abstract class FramedOrdinate : FramedAxis
    {
        protected FramedOrdinate(int graphLength) : base()
        {
            this.graphLength = graphLength;
            yCoordinatePadding = MainView.GetYCoordinatePadding();
        }

        protected abstract string GetPaddedYCoordinate(int yCoordinate);

        protected readonly int graphLength;
        protected readonly int yCoordinatePadding;

        protected const string VerticalFrameComponent = "|";
    }
}
