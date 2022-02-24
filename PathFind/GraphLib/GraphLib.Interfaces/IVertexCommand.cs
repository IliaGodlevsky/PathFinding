using Commands.Interfaces;

namespace GraphLib.Interfaces
{
    public interface IVertexCommand : IExecutable<IVertex>, ICanCheck<IVertex>
    {

    }
}
