namespace GraphLib.Serialization.Interfaces
{
    /// <summary>
    /// Provides constant cryptography 
    /// keys and initializing vector 
    /// for crypto algorithms
    /// </summary>
    public interface ICrypto
    {
        byte[] Key { get; }
        byte[] IV { get; }
    }
}
