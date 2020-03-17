using SearchAlgorythms.Top;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms.GraphCreateAlgorythm
{
    public class OnInfoGraphCreater : ICreateAlgorythm
    {
        private readonly Button[,] buttons = null;

        public OnInfoGraphCreater(GraphTopInfo[,] info,int buttonWidth,
            int buttonHeight, int placeBetweenButtons)
        {
            if (info == null)
                return;
            int graphWidth = info.GetLength(0);
            int graphHeight = info.Length / info.GetLength(0);
            buttons = new Button[graphWidth, graphHeight];
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
                    buttons[i, j].Size = new Size(buttonWidth, buttonHeight);
                    buttons[i, j].Location = new Point((i + 1) *
                        placeBetweenButtons + 150, (j + 1) * placeBetweenButtons);
                }
            }
        }

        public Button[,] GetGraph()
        {
            return buttons;
        }
    }
}
