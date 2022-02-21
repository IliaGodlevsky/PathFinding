using Common.Extensions;
using EnumerationValues.Extensions;
using EnumerationValues.Interface;
using System;
using WPFVersion3D.Attributes;
using WPFVersion3D.Enums;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Extensions
{
    internal static class EnumValuesExtensions
    {
        public static Tuple<string, IAnimationSpeed>[] ToAnimationSpeedTuples(this IEnumValues<AnimationSpeeds> values)
        {
            string Description(AnimationSpeeds speed) => speed.GetDescriptionAttributeValueOrDefault();
            IAnimationSpeed Speed(AnimationSpeeds speed) => speed.GetAttributeOrNull<BaseAnimationSpeed>();
            return values.ToTupleCollection(Description, Speed);
        }
    }
}
