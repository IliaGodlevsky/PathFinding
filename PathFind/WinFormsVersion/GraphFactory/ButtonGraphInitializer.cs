using GraphLibrary.GraphFactory;
using SearchAlgorythms.Top;
using System.Drawing;

namespace SearchAlgorythms.GraphFactory
{
    public class ButtonGraphInitializer : AbstractGraphInitializer
    {

        public ButtonGraphInitializer(GraphTopInfo[,] info, int placeBetweenTops)
            : base(info, placeBetweenTops)
        {
           
        }

        public override IGraphTop CreateGraphTop(GraphTopInfo info)
        {
            return new GraphTop(info);
        }

        public override void SetGraph(int width, int height)
        {
            buttons = new GraphTop[width, height];
        }
    }
}
