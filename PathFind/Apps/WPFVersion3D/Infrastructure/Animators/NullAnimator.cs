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
            const string callerClassName = nameof(NullAnimator);
            const string calledMethodName = nameof(ApplyAnimation);
            var message = $"{callerClassName} called {calledMethodName}";
            throw new Exception(message);
        }
    }
}
