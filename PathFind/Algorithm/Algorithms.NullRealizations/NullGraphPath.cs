using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algorithm.NullRealizations
{
    [Null]
    [DebuggerDisplay("Null")]
    public sealed class NullGraphPath : Singleton<NullGraphPath, IGraphPath>, IGraphPath
    {
        public double Cost => default;

        public int Count => default;

        private NullGraphPath()
        {

        }

        public IEnumerator<IVertex> GetEnumerator()
        {
            return Enumerable
                .Empty<IVertex>()
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
