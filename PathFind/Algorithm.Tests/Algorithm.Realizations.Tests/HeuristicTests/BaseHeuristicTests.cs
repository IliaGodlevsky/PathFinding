using Algorithm.Interfaces;
using GraphLib.Interfaces;
using Moq;

namespace Algorithm.Realizations.Tests.HeuristicTests
{
    public abstract class BaseHeuristicTests
    {
        protected abstract IHeuristic Heuristic { get; }

        private readonly Mock<IVertex> vertexFromMock;
        private readonly Mock<IVertex> vertexToMock;
        private readonly Mock<ICoordinate> coordinateFromMock;
        private readonly Mock<ICoordinate> coordinateToMock;

        private IVertex FirstVertex => vertexFromMock.Object;
        private IVertex SecondVertex => vertexToMock.Object;

        protected BaseHeuristicTests()
        {
            vertexFromMock = new Mock<IVertex>();
            vertexToMock = new Mock<IVertex>();
            coordinateFromMock = new Mock<ICoordinate>();
            coordinateToMock = new Mock<ICoordinate>();
        }

        public virtual double Calculate_EqualNumberOfCoordinateValues_ReturnsValidValue(
            int[] fromVertexCoordinateValues, int[] toVertexCoordinateValues)
        {
            coordinateFromMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(fromVertexCoordinateValues);
            vertexFromMock.Setup(vertex => vertex.Position).Returns(coordinateFromMock.Object);
            coordinateToMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(toVertexCoordinateValues);
            vertexToMock.Setup(vertex => vertex.Position).Returns(coordinateToMock.Object);

            return Heuristic.Calculate(FirstVertex, SecondVertex);
        }
    }
}