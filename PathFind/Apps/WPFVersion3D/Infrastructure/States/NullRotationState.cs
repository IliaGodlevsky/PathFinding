using SingletonLib;
using WPFVersion3D.Interface;
using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Infrastructure.States
{
    internal sealed class NullRotationState : Singleton<NullRotationState, IRotationState>, IRotationState
    {
        public bool CanRotate => false;

        public void Activate(GraphFieldAxisRotatingViewModel model)
        {
            
        }

        private NullRotationState()
        {

        }
    }
}
