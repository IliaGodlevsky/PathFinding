using SearchAlgorythms;
using SearchAlgorythms.GraphFactory;
using SearchAlgorythms.Top;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVersion.GraphFactory
{
    public class OnInfoConsoleGraphFactory : IGraphFactory
    {
        private readonly ConsoleGraphTop[,] buttons = null;

        public OnInfoConsoleGraphFactory(GraphTopInfo[,] info)
        {
            if (info == null)
                return;
            int graphWidth = info.GetLength(0);
            int graphHeight = info.Length / info.GetLength(0);
            buttons = new ConsoleGraphTop[graphWidth, graphHeight];
            for (int i = 0; i < graphWidth; i++)
            {
                for (int j = 0; j < graphHeight; j++)
                {
                    buttons[i, j] = new ConsoleGraphTop(info[i, j])
                    {
                        Location = new Point(i, j)
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
