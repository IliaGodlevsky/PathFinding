using SearchAlgorythms.Top;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms
{
    public class NeighbourSetter
    {
        private Button[,] graph;
        private readonly int width;
        private readonly int height;

        public NeighbourSetter(int width, int height, Button[,] buttons)
        {
            graph = buttons;
            this.width = width;
            this.height = height;
        }

        public static KeyValuePair<int,int> GetCoordinates(Button button, 
            Button[,] buttons, int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++) 
                {
                    if (button.Location == buttons[i, j].Location)
                        return new KeyValuePair<int, int>(i, j);
                }
            }
            return new KeyValuePair<int, int>(width, height);
        }

        public void SetNeighbours(int xCoordinate, int yCoordinate)
        {
            var top = graph[xCoordinate, yCoordinate] as GraphTop;
            for (int i = xCoordinate - 1; i <= xCoordinate + 1; i++)
            {
                for (int j = yCoordinate - 1; j <= yCoordinate + 1; j++)
                {
                    if (i >= 0 && i < width && j >= 0 && j < height)
                    {
                        if (graph[i, j] is GraphTop neighbour &&
                            neighbour.BackColor != Color.FromName("Black"))
                            top?.AddNeighbour(neighbour);
                    }
                }
            }
            top?.GetNeighbours().Remove((GraphTop)graph[xCoordinate, yCoordinate]);
        }

        public void SetNeighbours()
        {
            for (int xCoordinate = 0; xCoordinate < width; xCoordinate++)
                for (int yCoordinate = 0; yCoordinate < height; yCoordinate++)
                    SetNeighbours(xCoordinate, yCoordinate);
        }
    }
}
