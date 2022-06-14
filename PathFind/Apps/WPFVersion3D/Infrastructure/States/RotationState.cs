using Autofac;
using GalaSoft.MvvmLight.Messaging;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Interface;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Infrastructure.States
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
