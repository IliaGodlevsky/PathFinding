﻿using GraphLibrary.DTO;
using GraphLibrary.Globals;
using GraphLibrary.GraphFactory;
using GraphLibrary.Graphs;
using GraphLibrary.GraphSerialization.GraphLoader.Interface;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.VertexBinding;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLibrary.GraphSerialization.GraphLoader
{
    public class GraphLoader : IGraphLoader
    {
        public event Action<string> OnBadLoad;

        public Graph GetGraph(string path, Func<VertexDto, IVertex> generator)
        {
            var formatter = new BinaryFormatter();
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                    Initialise((VertexDto[,])formatter.Deserialize(stream), generator);
            }
            catch (Exception ex)
            {
                graph = null;
                OnBadLoad?.Invoke(ex.Message);
            }
            return graph;
        }

        private void Initialise(VertexDto[,] info, Func<VertexDto, IVertex> generator)
        {
            if (info == null)
                return;
            var initializer = new GraphInfoInitializer(info, VertexSize.SIZE_BETWEEN_VERTICES);
            graph = initializer.GetGraph(generator);
            VertexBinder.ConnectVertices(graph);
        }

        private Graph graph = null;
    }
}
