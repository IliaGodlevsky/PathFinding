using GraphLib.Coordinates.Interface;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;

namespace GraphLib.Graphs.Factories.Abstractions
{
    public abstract class BaseGraphFactory : IGraphFactory
    {
        public BaseGraphFactory(int obstacleChance)
        {
            this.obstacleChance = obstacleChance;
            graph = new DefaultGraph();
        }

        public abstract IGraph CreateGraph(Func<IVertex> vertexFactory);

        static BaseGraphFactory()
        {
            rand = new Random();
        }

        protected abstract ICoordinate GetCoordinate(params int[] coordinates);

        protected void CreateVertex(Func<IVertex> vertexFactory, params int[] coordinates)
        {
            var indices = GetCoordinate(coordinates);

            graph[indices] = vertexFactory();
            graph[indices].Cost = rand.GetRandomValueCost();

            if (rand.IsObstacleChance(obstacleChance))
            {
                graph[indices].MarkAsObstacle();
            }

            graph[indices].Position = indices;
        }

        protected IGraph graph;
        protected int obstacleChance;

        private static readonly Random rand;        
    }
}
