using System;
using WPFVersion3D.Infrastructure.Animators.Interface;

namespace WPFVersion3D.Infrastructure.Animators
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
