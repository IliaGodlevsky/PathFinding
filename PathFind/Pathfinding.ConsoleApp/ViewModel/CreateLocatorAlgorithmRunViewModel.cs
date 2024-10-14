using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Undefined;
using ReactiveUI;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class CreateLocatorAlgorithmRunViewModel : CreateRunButtonViewModel
    {
        public CreateLocatorAlgorithmRunViewModel(IRequestService<VertexModel> service,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ILog logger) : base(service, messenger, logger)
        {
        }

        protected override string AlgorithmId { get; } = "Locator";

        protected override void AppendStatistics(RunStatisticsModel model)
        {
            
        }

        protected override PathfindingProcess GetAlgorithm(IEnumerable<VertexModel> pathfindingRange)
        {
            return new LocatorAlgorithm(pathfindingRange);
        }
    }
}
