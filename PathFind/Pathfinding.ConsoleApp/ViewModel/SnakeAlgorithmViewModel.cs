using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class SnakeAlgorithmViewModel : PathfindingProcessViewModel
    {
        public SnakeAlgorithmViewModel(
            IRequestService<GraphVertexModel> service,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ILog logger) : base(service, messenger, logger)
        {
        }

        public override string AlgorithmId { get; } = AlgorithmNames.Snake;

        protected override PathfindingProcess GetAlgorithm(IEnumerable<GraphVertexModel> pathfindingRange)
        {
            return new SnakeAlgorithm(pathfindingRange);
        }
    }
}
