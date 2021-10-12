using GraphLib.Interfaces;
using GraphLib.TestRealizations.TestFactories;
using GraphLib.TestRealizations.TestObjects;
using System.Linq;

namespace Algorithm.Algos.Benchmark
{
    public static class ParamsSource
    {
        private static readonly IGraph graph10x10;
        private static readonly IGraph graph20x20;
        private static readonly IGraph graph30x30;
        private static readonly IGraph graph40x40;
        private static readonly IGraph graph50x50;

        private static readonly IIntermediateEndPoints endPoints10x10;
        private static readonly IIntermediateEndPoints endPoints20x20;
        private static readonly IIntermediateEndPoints endPoints30x30;
        private static readonly IIntermediateEndPoints endPoints40x40;
        private static readonly IIntermediateEndPoints endPoints50x50;



        static ParamsSource()
        {
            var testGraphAssemble = new TestGraphAssemble();
            graph10x10 = testGraphAssemble.AssembleGraph(0, 10, 10);
            graph20x20 = testGraphAssemble.AssembleGraph(0, 20, 20);
            graph30x30 = testGraphAssemble.AssembleGraph(0, 30, 30);
            graph40x40 = testGraphAssemble.AssembleGraph(0, 40, 40);
            graph50x50 = testGraphAssemble.AssembleGraph(0, 50, 50);
            endPoints10x10 = CreateEndPoints(graph10x10);
            endPoints20x20 = CreateEndPoints(graph20x20);
            endPoints30x30 = CreateEndPoints(graph30x30);
            endPoints40x40 = CreateEndPoints(graph40x40);
            endPoints50x50 = CreateEndPoints(graph50x50);
        }

        private static IIntermediateEndPoints CreateEndPoints(IGraph graph)
        {
            var source = graph.Vertices.First();
            var target = graph.Vertices.Last();
            return new TestEndPoints(source, target);
        }
    }
}
