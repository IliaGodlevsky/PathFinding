using NUnit.Framework;
using System.Collections;

namespace GraphLib.Realizations.Tests
{
    [TestFixture]
    public class WeightableVertexCost
    {
        private const int VertexStartCost = 4;
        private VertexCost.WeightableVertexCost vertexCost;

        [SetUp]
        public void Setup()
        {
            vertexCost = new VertexCost.WeightableVertexCost(VertexStartCost);
        }

        [Test]
        public void MakeUnweighted_CurrentCostIs4_SetsCurrentCostTo1()
        {
            vertexCost.MakeUnweighted();

            Assert.AreEqual(vertexCost.CurrentCost, 1);
        }

        [Test]
        public void MakeWeighted_CurrentCostIs1_SetsCurrentCostTo4()
        {
            vertexCost.MakeUnweighted();
            vertexCost.MakeWeighted();

            Assert.AreEqual(vertexCost.CurrentCost, VertexStartCost);
        }

        [TestCaseSource(nameof(UnweightedCostViews))]
        public void ToString_UnweightedVertexCost_ReturnsUnweightedCostView(string view)
        {
            vertexCost.UnweightedCostView = view;
            vertexCost.MakeUnweighted();

            string str = vertexCost.ToString();

            Assert.AreEqual(str, view);
        }

        [Test]
        public void ToString_WeightedVertexCost_ReturnsCurrentCostInString()
        {
            vertexCost.MakeWeighted();

            string str = vertexCost.ToString();

            Assert.AreEqual(str, vertexCost.CurrentCost.ToString());
        }

        private static readonly IEnumerable UnweightedCostViews
            = new string[] { "#", "%", " ", "^", "*" };
    }
}
