using SearchAlgorithms.Top;

namespace SearchAlgorithms.Algorithms.IGraphFactory
{
    public class OnInfoButtonGraphFactory : IGraphFactory
    {
        private readonly IGraphTop[,] buttons = null;

        public OnInfoButtonGraphFactory(IGraphTopInfo[,] info, int placeBetweenButtons)
        {
            if (info == null)
                return;
            int graphWidth = info.GetLength(0);
            int graphHeight = info.Length / info.GetLength(0);
            buttons = new IGraphTop[graphWidth, graphHeight];
            for (int i = 0; i < graphWidth; i++)
                for (int j = 0; j < graphHeight; j++)
                    buttons[i, j] = new GraphTop(info[i, j]);           
        }

        public IGraphTop[,] GetGraph()
        {
            return buttons;
        }
    }
}
