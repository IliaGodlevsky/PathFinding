using Common.Extensions;
using ConsoleVersion.Model;
using ConsoleVersion.ViewModel;
using EnumerationValues.Realizations;
using GraphLib.Interfaces;
using GraphLib.Realizations.Enums;
using GraphLib.Realizations.SmoothLevel;
using System.Linq;

namespace ConsoleVersion.Views
{
    internal sealed class GraphSmoothView : View
    {
        public GraphSmoothView(GraphSmoothViewModel model, CustomSmoothLevel smoothLevel) : base(model)
        {
            var customDescription = smoothLevel.GetDescription();
            var enumValues = EnumValuesWithoutIgnored<SmoothLevels>.Create().Values;
            var smoothLevelNames = enumValues
                .Select(item => item.GetDescription())
                .Append(customDescription);
            var menuList = new MenuList(smoothLevelNames);
            model.SmoothLevels = enumValues
                .Select(item => item.GetAttributeOrNull<SmoothLevelAttribute>())
                .Append<ISmoothLevel>(smoothLevel)
                .ToArray();
            model.ChooseSmoothLevelMsg = menuList + "\nChoose smooth level: ";
        }
    }
}
