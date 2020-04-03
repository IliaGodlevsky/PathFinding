using SearchAlgorythms.Top;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.Graph
{
    public class ButtonGraph : IGraph
    {
        public event MouseEventHandler SetStart;
        public event MouseEventHandler SetEnd;

        private IGraphTop[,] buttons;

        public IGraphTop[,] GetArray()
        {
            return buttons;
        }

        public ButtonGraph(IGraphTop[,] buttons)
        {
            this.buttons = buttons;
            Start = null;
            End = null;
        }

        public GraphTopInfo[,] GetInfo()
        {
            GraphTopInfo[,] info = new GraphTopInfo[GetWidth(), GetHeight()];
            for (int i = 0; i < GetWidth(); i++)
                for (int j = 0; j < GetHeight(); j++)
                    info[i, j] = buttons[i, j].GetInfo();
            return info;
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

        public IGraphTop Start { get; set; }
        public IGraphTop End { get; set; }

        public int GetSize()
        {
            if (GetWidth() == 0 || GetHeight() == 0)
                return 0;
            else
                return GetHeight() * GetWidth();
        }

        public IGraphTop this [int width, int height]
        {
            get { return buttons[width, height]; }
            set { buttons[width, height] = value; }
        }

        public void Insert(IGraphTop top)
        {
            var coordiantes = GetIndexes(top);
            buttons[coordiantes.X, coordiantes.Y] = top;
            NeigbourSetter setter = new NeigbourSetter(buttons);
            setter.SetNeighbours(coordiantes.X, coordiantes.Y);
        }

        public int GetObstaclePercent()
        {
            int numberOfObstacles = 0;
            foreach (var top in buttons)
                if (top.IsObstacle)
                    numberOfObstacles++;
            return numberOfObstacles * 100 / GetSize();
        }

        public Point GetIndexes(IGraphTop top)
        {
            for (int i = 0; i < GetWidth(); i++)
            {
                for (int j = 0; j < GetHeight(); j++)
                {
                    if ((top as GraphTop).Location 
                        == (buttons[i, j] as GraphTop).Location)
                        return new Point(i, j);
                }
            }
            return new Point(GetWidth(), GetHeight());
        }

        public void Refresh()
        {
            if (buttons == null)
                return;
            foreach(var top in buttons)
            {
                if (!top.IsObstacle)
                    top.SetToDefault();
                (top as GraphTop).MouseClick -= SetStart;
                (top as GraphTop).MouseClick -= SetEnd;
                (top as GraphTop).MouseClick += SetStart;
            }
            Start = null;
            End = null;
        }

        public IEnumerator GetEnumerator()
        {
            return buttons.GetEnumerator();
        }
    }
}
