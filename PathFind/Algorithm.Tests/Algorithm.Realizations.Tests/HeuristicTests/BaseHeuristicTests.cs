using Algorithm.Interfaces;
using GraphLib.Interfaces;
using GraphLib.TestRealizations.TestObjects;
using Moq;

namespace Algorithm.Realizations.Tests.HeuristicTests
{
    public abstract class BaseHeuristicTests
    {
        protected abstract IHeuristic Heuristic { get; }

        private readonly Mock<IVertex> vertexFromMock;
        private readonly Mock<IVertex> vertexToMock;

        private IVertex FirstVertex => vertexFromMock.Object;
        private IVertex SecondVertex => vertexToMock.Object;

        protected BaseHeuristicTests()
        {
            vertexFromMock = new Mock<IVertex>();
            vertexToMock = new Mock<IVertex>();
        }

        public virtual double Calculate_EqualNumberOfCoordinateValues_ReturnsValidValue(
            int[] fromVertexCoordinateValues, int[] toVertexCoordinateValues)
        {
            var fromCoordinate = new TestCoordinate(fromVertexCoordinateValues);
            var toCoordinate = new TestCoordinate(toVertexCoordinateValues);
            vertexFromMock.Setup(vertex => vertex.Position).Returns(fromCoordinate);
            vertexToMock.Setup(vertex => vertex.Position).Returns(toCoordinate);

            return Heuristic.Calculate(FirstVertex, SecondVertex);
        }
    }
}