using System.Collections;
using System.Drawing;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Graph
{
    public abstract class AbstractGraph : IEnumerable
    {
        public AbstractGraph(IGraphTop[,] tops)
        {
            buttons = tops;
        }

        protected IGraphTop[,] buttons;

        public IGraphTop this[int width, int height]
        {
            get { return buttons[width, height]; }
            set { buttons[width, height] = value; }
        }

        public IGraphTop End { get; set; }
        public IGraphTop Start { get; set; }

        public IGraphTop[,] GetArray()
        {
            return buttons;
        }

        public int GetHeight()
        {
            if (buttons != null)
                return buttons.Length / buttons.GetLength(0);
            else
                return 0;
        }

        public GraphTopInfo[,] GetInfo()
        {
            GraphTopInfo[,] info = new GraphTopInfo[GetWidth(), GetHeight()];
            for (int i = 0; i < GetWidth(); i++)
                for (int j = 0; j < GetHeight(); j++)
                    info[i, j] = buttons[i, j].GetInfo();
            return info;
        }

        public int GetSize()
        {
            if (GetWidth() == 0 || GetHeight() == 0)
                return 0;
            else
                return GetHeight() * GetWidth();
        }

        public int GetWidth()
        {
            if (buttons != null)
                return buttons.GetLength(0);
            else
                return 0;
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

        public abstract Point GetIndexes(IGraphTop top);
        public abstract void ToDefault(IGraphTop top);

        public void Refresh()
        {
            if (buttons == null)
                return;
            foreach (var top in buttons)            
                ToDefault(top);            
            Start = null;
            End = null;
        }

        public IEnumerator GetEnumerator()
        {
            return buttons.GetEnumerator();
        }
    }
}