using GraphLibrary.Graph;
using GraphLibrary.StatusSetter;
using GraphLibrary.Vertex;
using System;

namespace GraphLibrary.Model
{
    public abstract class AbstractGraphFiller
    {
        public void ChargeGraph(AbstractGraph graph) => SetVertexStatus(graph, ChargeVertex);

        public void RefreshGraph(AbstractGraph graph) => SetVertexStatus(graph, RefreshVertex);

        protected abstract IVertexStatusSetter GetStatusSetter(AbstractGraph graph);

        protected abstract void ChargeVertex(IVertex vertex, IVertexStatusSetter changer);
        protected abstract void RefreshVertex(IVertex vertex, IVertexStatusSetter changer);

        private void SetVertexStatus(AbstractGraph graph,
            Action<IVertex, IVertexStatusSetter> action)
        {
            var changer = GetStatusSetter(graph);
            for (int i = 0; i < graph.Width; i++)
                for (int j = 0; j < graph.Height; j++)
                    action(graph[i, j], changer);
        }
    }
}
