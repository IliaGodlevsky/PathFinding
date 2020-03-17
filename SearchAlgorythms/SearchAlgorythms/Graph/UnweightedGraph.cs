using SearchAlgorythms.Algorythms;
using SearchAlgorythms.Top;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SearchAlgorythms.Graph
{
    public class UnweightedGraph
    {
        private Button[,] buttons;

        public UnweightedGraph(GraphTopInfo[,] info)
        {

        }

        public UnweightedGraph(Button[,] buttons)
        {
            this.buttons = buttons;
        }

        public int GetWidth()
        {
            if (buttons != null)
                return buttons.GetLength(0);
            else
                return 0;
        }

        public int GetHeight()
        {
            if (buttons != null)
                return buttons.Length / buttons.GetLength(0);
            else
                return 0;
        }

        public int GetSize()
        {
            if (GetWidth() == 0 || GetHeight() == 0)
                return 0;
            else
                return GetHeight() * GetWidth();
        }

        public Button this [int width, int height]
        {
            get { return buttons[width, height]; }
            set { buttons[width, height] = value; }
        }

        public void Insert(Button top)
        {
            var coordiantes = GetCoordinates(top);
            buttons[coordiantes.Key, coordiantes.Value] = top;
            NeighbourSetter setter = new NeighbourSetter(GetWidth(), GetHeight(), buttons);
            setter.SetNeighbours(coordiantes.Key, coordiantes.Value);
        }

        public int ObstaclePercent()
        {
            int numberOfObstacles = 0;
            for (int i = 0; i < GetWidth(); i++) 
            {
                for (int j = 0; j < GetHeight(); j++)
                {
                    GraphTop top = buttons[i, j] as GraphTop;
                    if (top is null)
                        numberOfObstacles++;
                }
            }
            return (numberOfObstacles / GetSize());
        }

        public KeyValuePair<int,int> GetCoordinates(Button top)
        {
            for (int i = 0; i < GetWidth(); i++)
            {
                for (int j = 0; j < GetHeight(); j++)
                {
                    if (top.Location == buttons[i, j].Location)
                        return new KeyValuePair<int, int>(i, j);
                }
            }
            return new KeyValuePair<int, int>(GetWidth(), GetHeight());
        }
    }
}
