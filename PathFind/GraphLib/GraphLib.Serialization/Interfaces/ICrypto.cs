namespace GraphLib.Serialization.Interfaces
{
    public interface ICrypto
    {
        byte[] Key { get; }
        byte[] IV { get; }
    }
}
