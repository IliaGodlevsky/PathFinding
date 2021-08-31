using GraphLib.Base;
using GraphLib.Interfaces;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFVersion.Model
{
    internal sealed class EndPoints : BaseEndPoints
    {
        private static readonly SolidColorBrush ToReplaceMarkColor;

        static EndPoints()
        {
            ToReplaceMarkColor = new SolidColorBrush(new Color { A = 185, B = 0, R = 255, G = 140 });
        }

        protected override void SubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseLeftButtonDown += SetEndPoints;
                vert.MouseUp += MarkIntermediateToReplace;
            }
        }

        protected override void UnsubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseLeftButtonDown -= SetEndPoints;
                vert.MouseUp -= MarkIntermediateToReplace;
            }
        }

        protected override void MarkIntermediateToReplace(object sender, EventArgs e)
        {
            if (e is MouseButtonEventArgs args && args.ChangedButton == MouseButton.Middle)
            {
                base.MarkIntermediateToReplace(sender, e);
                if (sender is Vertex vertex && markedToReplaceIntermediates.Contains(vertex))
                {
                    vertex.Background = ToReplaceMarkColor;
                }
            }
        }
    }
}
