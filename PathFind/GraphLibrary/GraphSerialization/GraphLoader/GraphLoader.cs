using GraphLibrary.DTO;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.GraphSerialization.GraphLoader.Interface;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.VertexConnecting;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLibrary.GraphSerialization.GraphLoader
{
    /// <summary>
    /// Deserializes graph using BinaryFormatter class
    /// </summary>
    public class GraphLoader : IGraphLoader
    {
        public event Action<string> OnBadLoad;

        public IGraph LoadGraph(string path, Func<VertexDto, IVertex> converter)
        {
            var formatter = new BinaryFormatter();
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                { 
                    var verticesDto = (VertexDto[,])formatter.Deserialize(stream);
                    graph = GetGraphFromDto(verticesDto, converter);
                }
            }
            catch (Exception ex)
            {
                OnBadLoad?.Invoke(ex.Message);
            }
            return graph;
        }

        private IGraph GetGraphFromDto(VertexDto[,] verticesDto, Func<VertexDto, IVertex> dtoConverter)
        {
            graph = new Graph(verticesDto.Width(), verticesDto.Height());

            IVertex GetVertexFromDto(IVertex vertex)
            {
                var indices = graph.GetIndices(vertex);
                vertex = dtoConverter(verticesDto[indices.X, indices.Y]);
                vertex.Position = indices;
                return vertex;
            }

            graph.Array.Apply(GetVertexFromDto);
            VertexConnector.ConnectVertices(graph);

            return graph;
        }

        private IGraph graph = NullGraph.Instance;
    }
}
