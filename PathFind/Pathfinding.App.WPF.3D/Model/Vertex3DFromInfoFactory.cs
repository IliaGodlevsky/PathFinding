﻿using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Windows;
using System.Windows.Threading;

namespace Pathfinding.App.WPF._3D.Model
{
    internal sealed class Vertex3DFromInfoFactory : IVertexFromInfoFactory<Vertex3D>
    {
        private static readonly Dispatcher Dispatcher = Application.Current.Dispatcher;

        private readonly ITotalVisualization<Vertex3D> visualization;
        private readonly IModel3DFactory model3DFactory;

        public Vertex3DFromInfoFactory(ITotalVisualization<Vertex3D> visualization)
        {
            this.visualization = visualization;
        }

        public Vertex3DFromInfoFactory(IModel3DFactory model3DFactory, ITotalVisualization<Vertex3D> visualization)
        {
            this.model3DFactory = model3DFactory;
            this.visualization = visualization;
        }

        public Vertex3D CreateFrom(VertexSerializationInfo info)
        {
            return Dispatcher.Invoke(() =>
            {
                return new Vertex3D(info.Position, model3DFactory, visualization)
                {
                    IsObstacle = info.IsObstacle,
                    Cost = info.Cost
                };
            });
        }
    }
}