using SearchAlgorythms.Top;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace SearchAlgorythms.DelegatedMethods
{
    public static class DelegatedMethod
    {
        public static void Pause(int milliseconds)
        {
            var sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < milliseconds)
                Application.DoEvents();
            sw.Stop();
        }

        public static double GetMinValue(List<IGraphTop> tops, 
            Predicate <IGraphTop> predicate, Func<IGraphTop, double> func)
        {
            double min = 0;
            foreach (var top in tops)
            {
                if (!top.IsVisited)
                {
                    min = func(top);
                    break;
                }
            }
            foreach (var top in tops)
                if (min > func(top) && predicate(top))
                    min = func(top);
            return min;
        }

        public static double GetEuclideanDistance(IGraphTop top1, IGraphTop top2)
        {
            return Math.Sqrt(Math.Pow(top1.Location.X - top2.Location.X, 2) 
                + Math.Pow(top1.Location.Y - top2.Location.Y, 2));
        }

        public static double GetChebyshevDistance(IGraphTop top1, IGraphTop top2)
        {
            return Math.Max(Math.Abs(top1.Location.X - top2.Location.X), 
                Math.Abs(top1.Location.Y - top2.Location.Y));
        }
    }
}
