using Commands.Interfaces;

namespace GraphLib.Interfaces
{
    public interface IVerticesCommands : IExecutable<IVertex>, IUndoCommand
    {

    }
}
