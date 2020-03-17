using SearchAlgorythms.Top;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms
{
    public class GraphInfoCollector
    {
        public static bool IsObstacle(Button top)
        {
            GraphTop t = top as GraphTop;
            return t is null;
        }

        public GraphTopInfo[,] CollectInfo(Button[,] buttons)
        {
            if (buttons == null)
                return null;
            int graphWidth = buttons.GetLength(0);
            int graphHeight = buttons.Length / buttons.GetLength(0);
            GraphTopInfo[,] info = new GraphTopInfo[graphWidth, graphHeight];
            for (int i = 0; i < graphWidth; i++)
            {
                for (int j = 0; j < graphHeight; j++)
                {
                    info[i, j] = new GraphTopInfo
                    {
                        Location = new Point
                            (buttons[i, j].Location.X,
                        buttons[i, j].Location.Y),
                        Colour = "White"
                    };
                    if (IsObstacle(buttons[i,j]))
                    {
                        info[i, j].IsObstacle = true;
                        info[i, j].Colour = buttons[i, j].BackColor.Name;
                    }
                    else
                        info[i, j].IsObstacle = false;
                }
            }
            return info;
        }

        public void InitializeWithInfo(Button[,] buttons, GraphTopInfo[,] info)
        {
            int graphWidth = buttons.GetLength(0);
            int graphHeight = buttons.Length / buttons.GetLength(0);
            for (int i = 0; i < graphWidth; i++)
            {
                for (int j = 0; j < graphHeight; j++)
                {
                    if (info[i, j].IsObstacle)
                        buttons[i, j] = new Button();
                    else
                        buttons[i, j] = new GraphTop();
                    buttons[i, j].Location = info[i, j].Location;
                    buttons[i, j].BackColor = Color.FromName(info[i, j].Colour);
                }
            }
        }
    }
}
