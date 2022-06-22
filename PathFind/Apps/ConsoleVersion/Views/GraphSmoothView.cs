using ConsoleVersion.Extensions;
using ConsoleVersion.Model;
using ConsoleVersion.ViewModel;

namespace ConsoleVersion.Views
{
    internal sealed class GraphSmoothView : View
    {
        public GraphSmoothView(GraphSmoothViewModel model) : base(model)
        {
            var menuList = ConsoleSmoothLevels.Levels.CreateMenuList();
            model.ChooseSmoothLevelMsg = menuList + "\nChoose smooth level: ";
        }
    }
}
