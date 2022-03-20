namespace WPFVersion3D.Interface
{
    internal interface IPassValueMessage : IMessage
    {
        object Value { get; }
    }

    internal interface IPassValueMessage<TValue>
    {
        TValue Value { get; }
    }
}