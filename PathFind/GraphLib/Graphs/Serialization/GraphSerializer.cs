using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections;
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
            formatter = new BinaryFormatter();
        }

        public IGraph LoadGraph(string path, 
            Func<VertexInfo, IVertex> vertexConvertMethod)
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    var verticesInfo = (VertexInfoCollection)formatter.Deserialize(stream);
                    var dimensions = verticesInfo.DimensionsSizes.ToArray();

                    var graph = (IGraph)Activator.CreateInstance(typeof(TGraph), dimensions);

                    for (int i = 0; i < verticesInfo.Count(); i++)
                    {
                        graph[i] = vertexConvertMethod(verticesInfo.ElementAt(i));
                    }

                    VertexConnector.ConnectVertices(graph);

                    return graph;
                }
            }
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex.Message);
                return new DefaultGraph();
            }
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

        private readonly IFormatter formatter;
    }
}
