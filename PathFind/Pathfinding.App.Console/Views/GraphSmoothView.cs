using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.ViewModel;

namespace Pathfinding.App.Console.Views
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
