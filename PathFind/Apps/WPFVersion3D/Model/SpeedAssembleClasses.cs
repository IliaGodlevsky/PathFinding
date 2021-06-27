using AssembleClassesLib.Attributes;
using AssembleClassesLib.Realizations;
using AssembleClassesLib.Realizations.AssembleClassesImpl;
using Common.Extensions;
using System;
using System.Linq;
using WPFVersion3D.Infrastructure.AnimationSpeed;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model
{
    internal sealed class SpeedAssembleClasses : AssembleClasses
    {
        public SpeedAssembleClasses() 
            : base(new EmptyLoadPath(), new AllDirectories(), new AnimationSpeedLoadMethod())
        {
            speedType = typeof(IAnimationSpeed);
        }

        public override object Get(string name, params object[] parametres)
        {
            return base.Get(name, parametres) ?? new RandomSpeed();
        }

        protected override void LoadClassesFromAssemble()
        {
            types = loadMethod
                .Load(loadPath)
                .GetTypes()
                .Where(IsSpeed)
                .ToDictionary(Name);
        }

        private string Name(Type type)
        {
            return type.GetAttributeOrNull<ClassNameAttribute>()?.Name ?? type.FullName;
        }

        private bool IsSpeed(Type type)
        {
            return speedType.IsAssignableFrom(type) && !type.IsAbstract;
        }

        private readonly Type speedType;
    }
}
