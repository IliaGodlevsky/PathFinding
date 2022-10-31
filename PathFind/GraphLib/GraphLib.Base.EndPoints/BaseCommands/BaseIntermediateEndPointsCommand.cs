using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GraphLib.Base.EndPoints.BaseCommands
{
    internal abstract class BaseIntermediateEndPointsCommand<TVertex> : BaseEndPointsCommand<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        protected Collection<TVertex> Intermediates => endPoints.Intermediates;

        protected Collection<TVertex> MarkedToReplace => endPoints.MarkedToReplace;

        protected bool HasIsolatedIntermediates => HasIsolated(Intermediates);

        protected BaseIntermediateEndPointsCommand(BaseEndPoints<TVertex> endPoints)
            : base(endPoints)
        {

        }

        protected bool IsIntermediate(TVertex vertex)
        {
            return Intermediates.Contains(vertex);
        }

        protected bool IsMarkedToReplace(TVertex vertex)
        {
            return MarkedToReplace.Contains(vertex);
        }

        private static bool HasIsolated(IReadOnlyCollection<TVertex> collection)
        {
            return collection.Count > 0 && collection.Any(vertex => vertex.IsIsolated());
        }
    }
}