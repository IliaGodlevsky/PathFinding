using ConsoleVersion.Model.Vertex;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using System;

namespace ConsoleVersion.View
{
    internal class GraphCreateView : IView
    {
        private GraphCreatingViewModel Model { get; set; }
        public GraphCreateView(GraphCreatingViewModel model)
        {
            Model = model;
            Model.Messages = new Tuple<string, string, string>(
                ConsoleVersionResources.PercentMsg,
                ConsoleVersionResources.WidthMsg,
                ConsoleVersionResources.HeightMsg);
        }

        public void Start()
        {
            Model.CreateGraph(() => new ConsoleVertex());
        }
    }
}
