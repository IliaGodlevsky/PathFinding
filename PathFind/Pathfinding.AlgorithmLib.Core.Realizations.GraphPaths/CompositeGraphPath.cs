using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Core.Realizations.GraphPaths
{
    public sealed class CompositeGraphPath : IGraphPath
    {
        private readonly IReadOnlyCollection<IGraphPath> paths;
        private readonly Lazy<IReadOnlyCollection<ICoordinate>> path;
        private readonly Lazy<int> count;
        private readonly Lazy<double> cost;

        private IReadOnlyCollection<ICoordinate> Path => path.Value;

        public int Count => count.Value;

        public double Cost => cost.Value;

        public CompositeGraphPath(IReadOnlyCollection<IGraphPath> paths)
        {
            this.paths = paths;
            this.path = new Lazy<IReadOnlyCollection<ICoordinate>>(GetPath);
            this.count = new Lazy<int>(GetCount);
            this.cost = new Lazy<double>(GetCost);
        }

        private IReadOnlyCollection<ICoordinate> GetPath()
        {
            return paths.SelectMany(p => p.Reverse()).ToReadOnly();
        }

        private int GetCount()
        {
            return Path.Count == 0 ? 0 : paths.Sum(p => p.Count);
        }

        private double GetCost()
        {
            return Path.Count == 0 ? 0 : paths.Sum(p => p.Cost);
        }

        public IEnumerator<ICoordinate> GetEnumerator()
        {
            return Path.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}