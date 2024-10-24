using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface.Models.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.Test
{
    internal static class ModelBuilder
    {
        public static GraphModel<GraphVertexModel> CreateGraphModel() => new GraphModel<GraphVertexModel>();

        public static GraphModel<GraphVertexModel> WithEmptyGraph(this GraphModel<GraphVertexModel> graphModel)
        {
            graphModel.Graph = Graph<GraphVertexModel>.Empty;
            return graphModel;
        }

        public static GraphModel<GraphVertexModel> WithId(this GraphModel<GraphVertexModel> graphModel, int id)
        {
            graphModel.Id = id;
            return graphModel;
        }

        public static GraphModel<GraphVertexModel> WithNeighborhood(this GraphModel<GraphVertexModel> graphModel, string neighborhood)
        {
            graphModel.Neighborhood = neighborhood;
            return graphModel;
        }

        public static GraphModel<GraphVertexModel> WithSmoothLevel(this GraphModel<GraphVertexModel> graphModel, string smooth)
        {
            graphModel.SmoothLevel = smooth;
            return graphModel;
        }

        public static GraphModel<GraphVertexModel> WithName(this GraphModel<GraphVertexModel> graphModel, string name)
        {
            graphModel.Name = name;
            return graphModel;
        }

        public static GraphModel<GraphVertexModel> CreateEmptyModel()
        {
            return CreateGraphModel()
                .WithId(1)
                .WithName("Test")
                .WithNeighborhood(NeighborhoodNames.Moore)
                .WithEmptyGraph()
                .WithSmoothLevel("Test");
        }
    }
}
