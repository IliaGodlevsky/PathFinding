namespace WPFVersion3D.Interface
{
    internal interface IPassValueMessage<TValue>
    {
        TValue Value { get; }
    }
}