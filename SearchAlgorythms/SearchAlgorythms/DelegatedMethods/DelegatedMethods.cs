using SearchAlgorythms.Top;
using System;
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
        }

        public static double GetEuclideanDistance(IGraphTop top1, IGraphTop top2)
        {
            double a = Math.Pow(top1.Location.X - top2.Location.X, 2);
            double b = Math.Pow(top1.Location.Y - top2.Location.Y, 2);
            return Math.Sqrt(a + b);
        }

        public static double GetChebyshevDistance(IGraphTop top1, IGraphTop top2)
        {
            int a = Math.Abs(top1.Location.X - top2.Location.X);
            int b = Math.Abs(top1.Location.Y - top2.Location.Y);
            return Math.Max(a, b);
        }
    }
}
