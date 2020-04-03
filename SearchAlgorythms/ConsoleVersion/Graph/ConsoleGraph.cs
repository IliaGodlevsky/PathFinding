using System.Collections;
using System.Drawing;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Graph
{
    public class ConsoleGraph : IGraph
    {
        private IGraphTop[,] buttons;

        public IGraphTop[,] GetArray()
        {
            return buttons;
        }

        public ConsoleGraph(IGraphTop[,] buttons)
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

        public IGraphTop this[int width, int height]
        {
            get { return buttons[width, height]; }
            set { buttons[width, height] = value; }
        }

        public void Insert(IGraphTop top)
        {
            var coordiantes = new Point(top.Location.X, top.Location.Y);
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

        public void Refresh()
        {
            if (buttons == null)
                return;
            foreach (var top in buttons)
            {
                if (!top.IsObstacle)
                    top.SetToDefault();
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
