namespace WPFVersion.Interface
{
    public interface ICache<T>
    {
        T Cached { get; }
    }
}
