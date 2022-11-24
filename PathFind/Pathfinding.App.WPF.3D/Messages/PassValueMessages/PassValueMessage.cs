using Pathfinding.App.WPF._3D.Interface;

namespace Pathfinding.App.WPF._3D.Messages.PassValueMessages
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