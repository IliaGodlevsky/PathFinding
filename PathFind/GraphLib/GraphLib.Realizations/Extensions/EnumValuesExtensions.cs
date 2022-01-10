using Common.Extensions;
using EnumerationValues.Extensions;
using EnumerationValues.Interface;
using GraphLib.Interfaces;
using GraphLib.Realizations.Enums;
using GraphLib.Realizations.SmoothLevel;
using System;

namespace GraphLib.Realizations.Extensions
{
    public static class EnumValuesExtensions
    {
        public static Tuple<string, ISmoothLevel>[] ToSmoothLevelTuples(this IEnumValues<SmoothLevels> values)
        {
            string Description(SmoothLevels level) => level.GetDescriptionAttributeValueOrEmpty();
            ISmoothLevel Level(SmoothLevels level) => level.GetAttributeOrNull<SmoothLevelAttribute>();
            return values.ToTupleCollection(Description, Level);
        }
    }
}
