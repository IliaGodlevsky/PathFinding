using Commands.Interfaces;

namespace GraphLib.Interfaces
{
    public interface IVertexCommand<TVertex> : IExecutable<TVertex>, IExecutionCheck<TVertex>
        where TVertex : IVertex
    {

    }
}
