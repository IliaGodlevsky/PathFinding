using ConsoleVersion.Model;
using ConsoleVersion.ViewModel;
using System.Linq;

namespace ConsoleVersion.Views
{
    internal sealed class GraphSmoothView : View
    {
        public GraphSmoothView(GraphSmoothViewModel model) : base(model)
        {
            var smoothLevelNames = ConsoleSmoothLevels.Levels.Select(level => level.ToString());
            var menuList = new MenuList(smoothLevelNames);
            model.SmoothLevels = ConsoleSmoothLevels.Levels;
            model.ChooseSmoothLevelMsg = menuList + "\nChoose smooth level: ";
        }
    }
}
