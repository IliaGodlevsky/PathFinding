using GraphLib.Interfaces;
using System;

namespace GraphLib.NullRealizations.NullObjects
{
    public sealed class NullVisualizable : IVisualizable
    {
        public static IVisualizable Instance => instance.Value;

        public bool IsVisualizedAsPath => false;

        public bool IsVisualizedAsEndPoint => false;

        public void VisualizeAsEnqueued()
        {
            
        }

        public void VisualizeAsIntermediate()
        {
            
        }

        public void VisualizeAsMarkedToReplaceIntermediate()
        {
            
        }

        public void VisualizeAsObstacle()
        {
           
        }

        public void VisualizeAsPath()
        {
            
        }

        public void VisualizeAsRegular()
        {
            
        }

        public void VisualizeAsSource()
        {
            
        }

        public void VisualizeAsTarget()
        {
            
        }

        public void VisualizeAsVisited()
        {
            
        }

        private NullVisualizable()
        {

        }

        private static readonly Lazy<IVisualizable> instance = new Lazy<IVisualizable>(() => new NullVisualizable());
    }
}