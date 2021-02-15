using GraphLib.Realizations.VertexCost;
using NUnit.Framework;
using System.Collections;

namespace GraphLib.Realizations.Tests
{
    [TestFixture]
    public class CostTest
    {
        private const int VertexStartCost = 4;
        private Cost vertexCost;

        [SetUp]
        public void Setup()
        {
            vertexCost = new Cost(VertexStartCost);
        }

        [Test]
        public void MakeUnweighted_CurrentCostIs4_SetsCurrentCostTo1()
        {
            vertexCost.MakeUnWeighted();

            Assert.AreEqual(vertexCost.CurrentCost, 1);
        }

        [Test]
        public void MakeWeighted_CurrentCostIs1_SetsCurrentCostTo4()
        {
            vertexCost.MakeUnWeighted();
            vertexCost.MakeWeighted();

            Assert.AreEqual(vertexCost.CurrentCost, VertexStartCost);
        }

        [TestCaseSource(nameof(UnweightedCostViews))]
        public void ToString_UnweightedVertexCost_ReturnsUnweightedCostView(string view)
        {
            vertexCost.UnweightedCostView = view;
            vertexCost.MakeUnWeighted();

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
