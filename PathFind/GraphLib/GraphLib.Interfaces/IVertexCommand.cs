using Common.Interface;

namespace GraphLib.Interfaces
{
    public interface IVertexCommand : IExecutable<IVertex>
    {
        bool CanExecute(IVertex vertex);
    }
}
