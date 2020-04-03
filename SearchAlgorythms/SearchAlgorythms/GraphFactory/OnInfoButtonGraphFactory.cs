using SearchAlgorythms.Top;
using System.Drawing;

namespace SearchAlgorythms.GraphFactory
{
    public class OnInfoButtonGraphFactory : IGraphFactory
    {
        private readonly IGraphTop[,] buttons = null;

        public OnInfoButtonGraphFactory(GraphTopInfo[,] info, int placeBetweenTops)
        {
            if (info == null)
                return;
            int graphWidth = info.GetLength(0);
            int graphHeight = info.Length / info.GetLength(0);
            buttons = new IGraphTop[graphWidth, graphHeight];
            for (int i = 0; i < graphWidth; i++)
            {
                for (int j = 0; j < graphHeight; j++)
                {
                    buttons[i, j] = new GraphTop(info[i, j])
                    {
                        Location = new Point(i * placeBetweenTops, j * placeBetweenTops)
                    };
                }
            }
        }

        public IGraphTop[,] GetGraph()
        {
            return buttons;
        }
    }
}
