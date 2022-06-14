using WPFVersion3D.ViewModel;

namespace WPFVersion3D.Interface
{
    internal interface IRotationState
    {
        void Activate(GraphFieldAxisRotatingViewModel model);

        bool CanRotate { get; }
    }
}
