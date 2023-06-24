using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._3D.DependencyInjection;
using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.App.WPF._3D.ViewModel;

namespace Pathfinding.App.WPF._3D.Infrastructure.States
{
    internal abstract class RotationState : IRotationState
    {
        protected readonly IMessenger messenger;

        public abstract bool CanRotate { get; }

        protected RotationState()
        {
            messenger = DI.Container.Resolve<IMessenger>();
        }

        public abstract void Activate(GraphFieldAxisRotatingViewModel model);
    }
}
