using Pathfinding.App.WPF._3D.ViewModel;
using System;

namespace Pathfinding.App.WPF._3D.Messages.PassValueMessages
{
    internal sealed class RemoveAlgorithmMessage : PassValueMessage<Guid>
    {
        public RemoveAlgorithmMessage(Guid id) : base(id)
        {
        }
    }
}
