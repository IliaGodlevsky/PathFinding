using SearchAlgorythms.Top;
using System.Drawing;

namespace SearchAlgorythms.Algorythms.GraphCreateAlgorythm
{
    public class OnInfoButtonGraphCreater : ICreateAlgorythm
    {
        private readonly IGraphTop[,] buttons = null;

        public OnInfoButtonGraphCreater(IGraphTopInfo[,] info,int buttonWidth,
            int buttonHeight, int placeBetweenButtons)
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
                    buttons[i, j] = new GraphTop();
                    if (info[i, j].IsObstacle)
                    {
                        buttons[i, j].IsObstacle = true;
                        buttons[i, j].MarkAsObstacle();
                    }
                    GraphTopInfo graphInfo = info[i, j] as GraphTopInfo;
                    GraphTop top = buttons[i, j] as GraphTop;
                    top.Location = graphInfo.Location;
                    top.BackColor = Color.FromName(graphInfo.Colour);
                    top.Size = new Size(buttonWidth, buttonHeight);
                    top.Location = new Point((i + 1) *
                        placeBetweenButtons + 150, (j + 1) * placeBetweenButtons);
                    top.Text = graphInfo.Text;
                }
            }
        }

        public IGraphTop[,] GetGraph()
        {
            return buttons;
        }
    }
}
