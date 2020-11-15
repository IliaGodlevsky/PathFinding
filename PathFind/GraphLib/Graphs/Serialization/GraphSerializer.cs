using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections.Interface;
using GraphLib.Graphs.Serialization.Interfaces;
using GraphLib.Info;
using GraphLib.Vertex.Interface;
using GraphLib.VertexConnecting;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLib.Graphs.Serialization
{
    public class GraphSerializer<TGraph> : IGraphSerializer
        where TGraph : IGraph
    {
        public event Action<string> OnExceptionCaught;

        public GraphSerializer(IFormatter formatter)
        {
            this.formatter = formatter;
        }

        public GraphSerializer()
        {
            graph = new DefaultGraph();
            formatter = new BinaryFormatter();
        }

        public IGraph LoadGraph(string path, Func<VertexInfo, IVertex> vertexFactory)
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    var verticesDto = (IVertexInfoCollection)formatter.Deserialize(stream);
                    graph = AssembleGraph(verticesDto, vertexFactory);
                }
            }
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex.Message);
            }

            return graph;
        }

        public void SaveGraph(IGraph graph, string path)
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(stream, graph.VertexInfoCollection);
                }
            }
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex.Message);
            }
        }

        private IGraph AssembleGraph(IVertexInfoCollection verticesInfo,
                    Func<VertexInfo, IVertex> dtoConverter)
        {
            var dimensions = verticesInfo.DimensionsSizes.ToArray();
            graph = (IGraph)Activator.CreateInstance(typeof(TGraph), dimensions);

            for (int i = 0; i < verticesInfo.Count(); i++)
            {
                graph[i] = dtoConverter(verticesInfo.ElementAt(i));
            }

            VertexConnector.ConnectVertices(graph);

            return graph;
        }

        protected IFormatter formatter;

        private IGraph graph;
    }
}
