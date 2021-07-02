using AssembleClassesLib.Realizations;
using AssembleClassesLib.Realizations.AssembleClassesImpl;
using System;
using WPFVersion3D.Infrastructure.AnimationSpeed;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model
{
    internal sealed class AnimationSpeedClasses : AssembleClasses
    {
        public AnimationSpeedClasses() 
            : base(new EmptyLoadPath(), 
                  new AllDirectories(), 
                  new AnimationSpeedLoadMethod())
        {
            speedType = typeof(IAnimationSpeed);
        }

        public override object Get(string name, params object[] parametres)
        {
            return base.Get(name, parametres) ?? new NullSpeed();
        }

        protected override string[] GetFiles()
        {
            return new string[] { string.Empty };
        }

        protected override bool IsRequiredType(Type type)
        {
            return speedType.IsAssignableFrom(type);
        }

        private readonly Type speedType;
    }
}
