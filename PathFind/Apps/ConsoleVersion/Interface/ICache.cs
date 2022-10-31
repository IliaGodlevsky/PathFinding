namespace ConsoleVersion.Interface
{
    internal interface ICache<T>
    {
        T Cached { get; }
    }
}
