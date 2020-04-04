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

        public int Height => buttons.Length / buttons.GetLength(0);

        public GraphTopInfo[,] GetInfo()
        {
            GraphTopInfo[,] info = new GraphTopInfo[Width, Height];
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    info[i, j] = buttons[i, j].GetInfo();
            return info;
        }

        public int Size => Height* Width;

        public int Width => buttons.GetLength(0);

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
            return numberOfObstacles * 100 / Size;
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