using ConsoleVersion.ViewModel;
using System;

namespace ConsoleVersion.View
{
    internal class GraphCreateView : IView
    {
        private CreateGraphViewModel Model { get; set; }
        public GraphCreateView(CreateGraphViewModel model)
        {
            Model = model;
            Model.Messages = new Tuple<string, string, string>(
                Res.PercentMsg, 
                Res.WidthMsg, 
                Res.HeightMsg);
        }

        public void Start()
        {
            Model.CreateGraph();
        }
    }
}
