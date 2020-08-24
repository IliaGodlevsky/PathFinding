using ConsoleVersion.Graph;
using ConsoleVersion.ViewModel;

namespace ConsoleVersion.View
{
    public class PathFindView : IView
    {
        public PathFindViewModel Model { get; set; }
        public PathFindView(PathFindViewModel model)
        {
            Model = model;
        }

        public void Start()
        {
            Model.PathFind();
        }
    }
}
