using System.Collections;
using System.Drawing;
using GraphLibrary.Extensions.MatrixExtension;
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

        public int Height => buttons.Height();

        public GraphTopInfo[,] Info => buttons.Accumulate(t => t.GetInfo());

        public int Size => buttons.Length;

        public int Width => buttons.Width();

        public void Insert(IGraphTop top)
        {
            var coordiantes = GetIndexes(top);
            buttons[coordiantes.X, coordiantes.Y] = top;
            NeigbourSetter setter = new NeigbourSetter(buttons);
            setter.SetNeighbours(coordiantes.X, coordiantes.Y);
        }

        public int ObstaclePercent => buttons.CountIf(t => t.IsObstacle) * 100 / Size;

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