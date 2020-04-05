using GraphLibrary.GraphFactory;
using SearchAlgorythms;
using SearchAlgorythms.Top;

namespace ConsoleVersion.GraphFactory
{
    public class ConsoleGraphInitializer : AbstractGraphInitializer
    {
        public ConsoleGraphInitializer(GraphTopInfo[,] info) : base(info, placeBetweenTops: 1)
        {

        }

        public override IGraphTop CreateGraphTop(GraphTopInfo info)
        {
            return new ConsoleGraphTop(info);
        }

        public override void SetGraph(int width, int height)
        {
            buttons = new ConsoleGraphTop[width, height];
        }
    }
}
