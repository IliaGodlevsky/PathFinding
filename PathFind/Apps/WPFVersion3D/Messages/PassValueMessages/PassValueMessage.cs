using WPFVersion3D.Interface;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal abstract class PassValueMessage<TValue> : IPassValueMessage<TValue>
    {
        public TValue Value { get; }

        public PassValueMessage(TValue value)
        {
            Value = value;
        }
    }
}