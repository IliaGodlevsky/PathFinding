using Common.Extensions;
using EnumerationValues.Extensions;
using EnumerationValues.Interface;
using EnumerationValues.Realizations;
using System;
using WPFVersion3D.Attributes;
using WPFVersion3D.Enums;

namespace WPFVersion3D.Extensions
{
    internal static class EnumValuesExtensions
    {
        public static Tuple<string, BaseAnimationSpeed>[] ToAnimationSpeedTuples(this IEnumValues<AnimationSpeeds> values)
        {
            var enumValuesWithoutIgnored = new EnumValuesWithoutIgnored<AnimationSpeeds>(values);
            string Description(AnimationSpeeds speed) => speed.GetDescriptionAttributeValueOrTypeName();
            BaseAnimationSpeed Speed(AnimationSpeeds speed) => speed.GetAttributeOrNull<BaseAnimationSpeed>();
            return enumValuesWithoutIgnored.ToTupleCollection(Description, Speed);
        }
    }
}
