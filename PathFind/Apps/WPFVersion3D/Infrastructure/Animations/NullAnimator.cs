using System;
using WPFVersion3D.Infrastructure.Animations.Interface;

namespace WPFVersion3D.Infrastructure.Animations
{
    internal sealed class NullAnimator : IAnimator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ApplyAnimation()
        {
            throw new Exception($"{nameof(NullAnimator)} called animation method");
        }
    }
}
