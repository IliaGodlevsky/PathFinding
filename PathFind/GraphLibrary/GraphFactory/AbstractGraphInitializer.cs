using SearchAlgorythms;
using SearchAlgorythms.Top;
using System.Drawing;

namespace GraphLibrary.GraphFactory
{
    public abstract class AbstractGraphInitializer : IGraphFactory
    {
        protected IGraphTop[,] buttons = null;

        public AbstractGraphInitializer(GraphTopInfo[,] info, int placeBetweenTops)
        {
            if (info == null)
                return;
            int graphWidth = info.GetLength(0);
            int graphHeight = info.Length / info.GetLength(0);
            SetGraph(graphWidth, graphHeight);
            for (int i = 0; i < graphWidth; i++)
            {
                for (int j = 0; j < graphHeight; j++)
                {
                    buttons[i, j] = CreateGraphTop(info[i, j]);
                    buttons[i, j].Location = new Point(i * placeBetweenTops, 
                        j * placeBetweenTops);                    
                }
            }
        }

        public abstract IGraphTop CreateGraphTop( GraphTopInfo info);
        public abstract void SetGraph(int width, int height);

        public IGraphTop[,] GetGraph()
        {
            return buttons;
        }
       
    }
}
