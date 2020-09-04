using GraphLibrary.Collection;
using GraphLibrary.VertexEventHolder;
using GraphLibrary.Vertex;
using System;

namespace GraphLibrary.Model
{
    public abstract class AbstractVertexEventSetter
    {
        public void ChargeGraph(Graph graph) => SetEventsToVertex(graph, ChargeVertex);

        public void RefreshGraph(Graph graph)
        {
            graph.End = null;
            graph.Start = null;
            SetEventsToVertex(graph, RefreshVertex);
        }

        protected abstract IVertexEventHolder GetStatusSetter(Graph graph);

        protected abstract void ChargeVertex(IVertex vertex, IVertexEventHolder changer);
        protected abstract void RefreshVertex(IVertex vertex, IVertexEventHolder changer);

        private void SetEventsToVertex(Graph graph,
            Action<IVertex, IVertexEventHolder> action)
        {
            var changer = GetStatusSetter(graph);
            for (int i = 0; i < graph.Width; i++)
                for (int j = 0; j < graph.Height; j++)
                    action(graph[i, j], changer);
        }
    }
}
