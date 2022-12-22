namespace Pathfinding.GraphLib.Serialization.Core.Interface
{
    public interface ICrypto
    {
        byte[] Key { get; }

        byte[] IV { get; }
    }
}
