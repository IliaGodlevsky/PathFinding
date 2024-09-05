using Pathfinding.Domain.Interface;
using System.Collections;
using System.Text;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations
{
    internal sealed class TestPathfindingRange : IPathfindingRange<TestVertex>
    {
        public TestVertex Source { get; set; } = null!;

        public TestVertex Target { get; set; } = null!;

        public IList<TestVertex> Transit { get; } = new List<TestVertex>();

        public IEnumerator<TestVertex> GetEnumerator()
        {
            return Transit.Append(Target)
                .Prepend(Source)
                .Where(vertex => vertex != null)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
