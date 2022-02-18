using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System.Diagnostics;

namespace GraphLib.NullRealizations
{
    [Null]
    [DebuggerDisplay("Null")]
    public sealed class NullVisualizable : Singleton<NullVisualizable, IVisualizable>, IVisualizable
    {
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
    }
}