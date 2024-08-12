using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.ViewModel;
using Pathfinding.Infrastructure.Data.Extensions;
using ReactiveUI;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    public class GraphView : Terminal.Gui.View
    {
        private readonly IVertexBehavior behavior;

        private const int VertexWidth = 3;
        private const int VertexHeight = 1;

        public GraphView(GraphViewModel viewModel, 
            //IEnumerable<Terminal.Gui.View> childViews,
            IVertexBehavior behavior)
        {
            Data = viewModel;
            viewModel.WhenAnyValue(x => x.Graph).Subscribe(AddToCanvas);
            X = Y = 0;
            Width = Dim.Fill();
            Height = Dim.Fill();
            this.behavior = behavior;
            //Add(childViews.ToArray());
        }

        private void AddToCanvas(IEnumerable<VertexViewModel> vertices)
        {
            static VertexView CreateVertexView(VertexViewModel vertexViewModel)
            {
                return new (vertexViewModel)
                {
                    X = vertexViewModel.Position.GetX() * (VertexWidth + 1),
                    Y = vertexViewModel.Position.GetY() * VertexHeight,
                    Width = VertexWidth,
                    Height = VertexHeight
                };
            }
            Subviews.OfType<VertexView>().ForEach(behavior.RemoveBehavior);
            Clear();
            vertices
                .Select(CreateVertexView)
                .ForEach(behavior.AddBehavior)
                .ToArray()
                .ForWhole(Add);
        }
    }
}
