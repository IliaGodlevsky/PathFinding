using System;
using WPFVersion3D.Infrastructure.Animations.Interface;

namespace WPFVersion3D.Infrastructure.Animations
{
    internal class NullAnimator : IAnimator
    {
        public void ApplyAnimation()
        {
            throw new Exception($"{nameof(NullAnimator)} called animation method");
        }
    }
}
