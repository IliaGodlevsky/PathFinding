namespace WPFVersion3D.Messages
{
    internal abstract class PassValueMessage<TValue>
    {
        public TValue Value { get; }

        public PassValueMessage(TValue value)
        {
            Value = value;
        }
    }
}