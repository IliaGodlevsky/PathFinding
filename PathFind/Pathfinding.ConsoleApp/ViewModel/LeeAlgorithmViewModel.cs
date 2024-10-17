using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class LeeAlgorithmViewModel : PathfindingProcessViewModel
    {
        public LeeAlgorithmViewModel(
            IRequestService<GraphVertexModel> service,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger, 
            ILog logger)
            : base(service, messenger, logger)
        {
        }

        public override string AlgorithmId => AlgorithmNames.Lee;

        protected override PathfindingProcess GetAlgorithm(IEnumerable<GraphVertexModel> pathfindingRange)
        {
            return new LeeAlgorithm(pathfindingRange);
        }
    }
}
