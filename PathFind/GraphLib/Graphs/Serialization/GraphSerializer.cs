using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections;
using GraphLib.Graphs.Serialization.Interfaces;
using GraphLib.Info;
using GraphLib.Vertex.Interface;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using static Common.ObjectActivator;

namespace GraphLib.Graphs.Serialization
{
    public class GraphSerializer<TGraph> : IGraphSerializer
        where TGraph : class, IGraph
    {
        public event Action<string> OnExceptionCaught;

        public GraphSerializer()
        {
            formatter = new BinaryFormatter();
        }

        static GraphSerializer()
        {
            var ctor = typeof(TGraph).GetConstructor(typeof(int[]));
            RegisterConstructor<TGraph>(ctor);
        }

        public IGraph LoadGraph(Stream stream,
            Func<VertexSerializationInfo, IVertex> vertexConvertMethod)
        {
            try
            {                
                var verticesInfo = (GraphSerializationInfo)formatter.Deserialize(stream);
                var dimensions = verticesInfo.DimensionsSizes.ToArray();

                var activator = GetActivator<TGraph>();

                var graph = activator(dimensions);

                for (int i = 0; i < verticesInfo.Count(); i++)
                {
                    var vertexInfo = verticesInfo.ElementAt(i);
                    graph[i] = vertexConvertMethod(vertexInfo);
                }

                graph.ConnectVertices();

                return graph;

            }
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex.Message);
                return new NullGraph();
            }
        }

        public void SaveGraph(IGraph graph, Stream stream)
        {
            try
            {
                formatter.Serialize(stream, graph.SerializationInfo);
            }
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex.Message);
            }
        }

        private readonly IFormatter formatter;
    }
}
