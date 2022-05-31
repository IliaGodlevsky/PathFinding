using Common.Extensions;
using ConsoleVersion.Model;
using ConsoleVersion.ViewModel;
using EnumerationValues.Realizations;
using GraphLib.Realizations.Enums;
using System.Linq;

namespace ConsoleVersion.Views
{
    internal sealed class GraphSmoothView : View
    {
        public GraphSmoothView(GraphSmoothViewModel model) : base(model)
        {
            var enumValues = EnumValuesWithoutIgnored<SmoothLevels>.Create().Values;
            var smoothLevelNames = enumValues
                .Select(item => item.GetDescription())
                .ToArray();
            var menuList = new MenuList(smoothLevelNames);
            model.ChooseSmoothLevelMsg = menuList + "\nChoose smooth level: ";
        }
    }
}
