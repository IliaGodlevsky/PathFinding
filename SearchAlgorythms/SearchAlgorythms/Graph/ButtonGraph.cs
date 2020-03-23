using SearchAlgorythms.Algorythms;
using SearchAlgorythms.Top;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.Graph
{
    public class ButtonGraph : IGraph
    {
        public event EventHandler SetStart;
        public event EventHandler SetEnd;

        private IGraphTop[,] buttons;
        private BoundSetter boundSetter = new BoundSetter();

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

        public IGraphTopInfo[,] GetInfo()
        {
            IGraphTopInfo[,] info = new GraphTopInfo[GetWidth(), GetHeight()];
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
            buttons[coordiantes.Key, coordiantes.Value] = top;
            NeigbourSetter setter = new NeigbourSetter(buttons);
            setter.SetNeighbours(coordiantes.Key, coordiantes.Value);
        }

        public int GetObstaclePercent()
        {
            int numberOfObstacles = 0;
            foreach (var top in buttons)
                if (top.IsObstacle)
                    numberOfObstacles++;
            return numberOfObstacles * 100 / GetSize();
        }

        public KeyValuePair<int,int> GetIndexes(IGraphTop top)
        {
            for (int i = 0; i < GetWidth(); i++)
            {
                for (int j = 0; j < GetHeight(); j++)
                {
                    if ((top as GraphTop).Location 
                        == (buttons[i, j] as GraphTop).Location)
                        return new KeyValuePair<int, int>(i, j);
                }
            }
            return new KeyValuePair<int, int>(GetWidth(), GetHeight());
        }

        public void Refresh()
        {
            if (buttons == null)
                return;
            foreach(var top in buttons)
            {
                if (!top.IsObstacle)
                {
                    top.SetToDefault();
                    (top as GraphTop).Click -= SetStart;
                    (top as GraphTop).Click -= SetEnd;
                    (top as GraphTop).Click += SetStart;
                }
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
