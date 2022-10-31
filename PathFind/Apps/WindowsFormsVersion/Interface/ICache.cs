namespace WindowsFormsVersion.Interface
{
    public interface ICache<T>
    {
        T Cached { get; }
    }
}
