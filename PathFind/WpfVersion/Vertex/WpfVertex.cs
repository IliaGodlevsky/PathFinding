using GraphLibrary;
using GraphLibrary.Constants;
using GraphLibrary.Vertex;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using GraphLibrary.PauseMaker;

namespace WpfVersion.Vertex
{
    public class WpfVertex : Label, IVertex
    {
        public WpfVertex() : base()
        {
            Neighbours = new List<IVertex>();
            SetToDefault();
            IsObstacle = false;
            FontSize = 8.0f;
            Width = Const.WIN_FORMS_VERTEX_SIZE;
            Height = Const.WIN_FORMS_VERTEX_SIZE;
            HorizontalAlignment = HorizontalAlignment.Center;
        }

        public bool IsEnd { get; set ; }
        public bool IsObstacle { get; set; }

        public bool IsSimpleVertex => !IsEnd && !IsStart;

        public bool IsStart { get; set ; }
        public bool IsVisited { get; set; }
        public string Text
        {
            get
            {
                return Content.ToString();
            }
            set
            {
                Content = value;
            }
        }
        public List<IVertex> Neighbours { get; set; }
        public IVertex ParentVertex { get; set; }
        public double Value { get; set; }
        public System.Drawing.Point Location { get; set; }

        public VertexInfo Info => throw new NotImplementedException();

        public void MarkAsCurrentlyLooked()
        {
            Background = new SolidColorBrush(Colors.Magenta);
        }

        public void MarkAsEnd()
        {
            Background = new SolidColorBrush(Colors.Red);
        }

        public void MarkAsObstacle()
        {
            Background = new SolidColorBrush(Colors.Black);
            Text = "";
            IsObstacle = true;
        }

        public void MarkAsPath()
        {
            Background = new SolidColorBrush(Colors.Yellow);
        }

        public void MarkAsSimpleVertex()
        {
            if (!IsObstacle)
                Background = new SolidColorBrush(Colors.White);
        }

        public void MarkAsStart()
        {
            Background = new SolidColorBrush(Colors.Green);
        }

        public void MarkAsVisited()
        {
            Background = new SolidColorBrush(Colors.CadetBlue);
        }

        public void SetToDefault()
        {
            IsStart = false;
            IsEnd = false;
            IsVisited = false;
            Value = 0;
            MarkAsSimpleVertex();
            ParentVertex = null;
        }
    }
}
