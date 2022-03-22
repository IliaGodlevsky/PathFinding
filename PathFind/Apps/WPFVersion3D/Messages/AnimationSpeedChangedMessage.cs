using WPFVersion3D.Interface;

namespace WPFVersion3D.Messages
{
    internal sealed class AnimationSpeedChangedMessage : PassValueMessage<IAnimationSpeed>
    {
        public AnimationSpeedChangedMessage(IAnimationSpeed value)
            : base(value)
        {

        }
    }
}