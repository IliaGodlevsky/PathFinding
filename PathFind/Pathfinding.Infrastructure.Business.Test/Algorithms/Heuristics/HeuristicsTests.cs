using Moq;
using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Infrastructure.Business.Test.Algorithms.Heuristics
{
    public abstract class HeuristicsTests
    {
        protected abstract IHeuristic Heuristic { get; }

        public virtual double Calculate_ValidVertices_ReturnsValidResult(int[] coordinate1, int[] coordinate2)
        {
            var vertex1 = new Mock<IVertex>();
            var vertex2 = new Mock<IVertex>();
            vertex1.Setup(x => x.Position).Returns(new Coordinate(coordinate1));
            vertex2.Setup(x => x.Position).Returns(new Coordinate(coordinate2));

            return Heuristic.Calculate(vertex1.Object, vertex2.Object);
        }
    }
}
