namespace Pathfinding.Service.Interface
{
    public interface ICrypto
    {
        byte[] Key { get; }

        byte[] IV { get; }
    }
}
