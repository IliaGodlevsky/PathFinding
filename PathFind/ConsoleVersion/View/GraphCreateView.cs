using ConsoleVersion.ViewModel;

namespace ConsoleVersion.View
{
    public class GraphCreateView : IView
    {
        private CreateGraphViewModel Model { get; set; }
        public GraphCreateView(CreateGraphViewModel model)
        {
            Model = model;
        }

        public void Start()
        {
            Model.CreateGraph();
        }
    }
}
