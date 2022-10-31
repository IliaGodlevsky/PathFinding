using Commands.Interfaces;
using GraphLib.Base.EndPoints.Commands.EndPointsCommands;
using GraphLib.Base.EndPoints.Commands.UndoCommands;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Commands.VerticesCommands
{
    internal sealed class SetEndPointsCommands<TVertex> : BaseVerticesCommands<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        public SetEndPointsCommands(BaseEndPoints<TVertex> endPoints) : base(endPoints)
        {
        }

        protected override IEnumerable<IVertexCommand<TVertex>> GetCommands(BaseEndPoints<TVertex> endPoints)
        {
            yield return new UnsetIntermediateCommand<TVertex>(endPoints);
            yield return new UnsetTargetCommand<TVertex>(endPoints);
            yield return new UnsetSourceCommand<TVertex>(endPoints);
            yield return new SetSourceCommand<TVertex>(endPoints);
            yield return new SetTargetCommand<TVertex>(endPoints);
            yield return new SetIntermediateCommand<TVertex>(endPoints);
            yield return new ReplaceIntermediateCommand<TVertex>(endPoints);
            yield return new ReplaceIntermediateIsolatedCommand<TVertex>(endPoints);
            yield return new ReplaceIsolatedSourceCommand<TVertex>(endPoints);
            yield return new ReplaceIsolatedTargetCommand<TVertex>(endPoints);
        }

        protected override IEnumerable<IUndoCommand> GetUndoCommands(BaseEndPoints<TVertex> endPoints)
        {
            yield return new UndoSetIntermediatesCommand<TVertex>(endPoints);
            yield return new UndoSetTargetCommand<TVertex>(endPoints);
            yield return new UndoSetSourceCommand<TVertex>(endPoints);
        }
    }
}