namespace GraphLib.Interfaces
{
    public interface IVertexCommand : IExecutable<IVertex>
    {
        bool IsTrue(IVertex vertex);
    }
}
