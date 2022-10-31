using Commands.Interfaces;

namespace GraphLib.Interfaces
{
    public interface IVerticesCommands<TVertex> : IExecutable<TVertex>, IUndoCommand
        where TVertex : IVertex
    {

    }
}
