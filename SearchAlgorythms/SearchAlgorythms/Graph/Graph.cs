using SearchAlgorythms.Algorythms;
using SearchAlgorythms.ButtonExtension;
using SearchAlgorythms.Top;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.Graph
{
    public class Graph : IGraph
    {
        public event EventHandler SetStart;
        public event EventHandler SetEnd;
        public event MouseEventHandler SwitchRole;

        private Button[,] buttons;
        private BoundSetter boundSetter = new BoundSetter();

        private void MakeObstacle(ref Button top)
        {
            GraphTop button = top as GraphTop;
            if (!button.IsStart && !button.IsEnd)
            {
                Button prev = top as Button;
                boundSetter.BreakBoundsBetweenNeighbours(top);
                top = new Button
                {
                    Location = prev.Location,
                    BackColor = Color.FromName("Black")
                };
                top.Text = "";
                Insert(top);
            }
        }

        private void MakeTop(ref Button top)
        {
            Random rand = new Random();
            Button prev = top as Button;
            top = new GraphTop { Location = prev.Location };
            if (Start == null)
                top.Click += SetStart;
            else if (Start != null && End == null)
                top.Click += SetEnd;
            top.Text = (rand.Next(9) + 1).ToString();
            Insert(top);
            boundSetter.SetBoundsBetweenNeighbours(top);
        }

        public Button[,] GetArray()
        {
            return buttons;
        }

        public Graph(Button[,] buttons)
        {
            this.buttons = buttons;
            Start = null;
            End = null;
        }

        public void InitializeWith(GraphTopInfo[,] info)
        {
            int graphWidth = info.GetLength(0);
            int graphHeight = info.Length / info.GetLength(0);
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

        public GraphTop Start { get; set; }
        public GraphTop End { get; set; }

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
            var coordiantes = GetIndexes(top);
            buttons[coordiantes.Key, coordiantes.Value] = top;
            NeigbourSetter setter = new NeigbourSetter(buttons);
            setter.SetNeighbours(coordiantes.Key, coordiantes.Value);
        }

        public int GetObstaclePercent()
        {
            int numberOfObstacles = 0;
            for (int i = 0; i < GetWidth(); i++) 
            {
                for (int j = 0; j < GetHeight(); j++)
                {
                    if (buttons[i,j].IsObstacle())
                        numberOfObstacles++;
                }
            }
            return numberOfObstacles * 100 / GetSize();
        }

        public KeyValuePair<int,int> GetIndexes(Button top)
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

        public void Reverse(ref Button top)
        {
            Size size = top.Size;
            if (top.IsObstacle())
                MakeTop(ref top);
            else
                MakeObstacle(ref top);
            top.Size = size;
            top.MouseDown += SwitchRole;
        }

        public void Refresh()
        {
            if (buttons == null)
                return;
            for (int i = 0; i < GetWidth(); i++)
            {
                for (int j = 0; j < GetHeight(); j++)
                {
                    if (buttons[i, j] is GraphTop top)
                    {
                        top.SetToDefault();
                        buttons[i, j].Click -= SetStart;
                        buttons[i, j].Click -= SetEnd;
                        buttons[i, j].Click += SetStart;
                    }
                    buttons[i, j].MouseDown -= SwitchRole;
                    buttons[i, j].MouseDown += SwitchRole;
                }
            }
            Start = null;
            End = null;
        }
    }
}
