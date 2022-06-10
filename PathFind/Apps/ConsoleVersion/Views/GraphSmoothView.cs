using ConsoleVersion.Extensions;
using ConsoleVersion.Model;
using ConsoleVersion.ViewModel;
using System.Linq;

namespace ConsoleVersion.Views
{
    internal sealed class GraphSmoothView : View
    {
        public GraphSmoothView(GraphSmoothViewModel model) : base(model)
        {
            var menuList = ConsoleSmoothLevels.Levels.Select(level => level.ToString()).ToMenuList();
            model.ChooseSmoothLevelMsg = menuList + "\nChoose smooth level: ";
        }
    }
}
