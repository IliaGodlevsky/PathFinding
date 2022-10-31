using Commands.Interfaces;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal sealed class RestoreColorsCommands<TVertex> : BaseVerticesCommands<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public RestoreColorsCommands(BaseEndPoints<TVertex> endPoints) : base(endPoints)
        {

        }

        protected override IEnumerable<IVertexCommand<TVertex>> GetCommands(BaseEndPoints<TVertex> endPoints)
        {
            yield return new RestoreIntermediateColorCommand<TVertex>(endPoints);
            yield return new RestoreMarkedToReplaceColorCommand<TVertex>(endPoints);
            yield return new RestoreSourceColorCommand<TVertex>(endPoints);
            yield return new RestoreTargetColorCommand<TVertex>(endPoints);
        }

        protected override IEnumerable<IUndoCommand> GetUndoCommands(BaseEndPoints<TVertex> endPoints)
        {
            return Enumerable.Empty<IUndoCommand>();
        }
    }
}
