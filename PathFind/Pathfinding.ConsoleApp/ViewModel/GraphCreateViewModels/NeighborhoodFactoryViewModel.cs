using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.ConsoleApp.ViewModel.GraphCreateViewModels
{
    public sealed class NeighborhoodFactoryViewModel
    {
        private readonly IMessenger messenger;

        public INeighborhoodFactory NeighborhoodFactory { get; set; }

        public IReadOnlyDictionary<string, INeighborhoodFactory> Factories { get; }

        public NeighborhoodFactoryViewModel([KeyFilter(KeyFilters.ViewModels)]IMessenger messenger,
            IEnumerable<(string Name, INeighborhoodFactory Factory)> factories)
        {
            this.messenger = messenger;
            messenger.Register<GraphParametresRequest>(this, OnGraphParametresRequestRecieved);
            Factories = factories.ToDictionary(x => x.Name, x => x.Factory).AsReadOnly();
        }

        private void OnGraphParametresRequestRecieved(object recipient, GraphParametresRequest request)
        {
            request.NeighborhoodFactory = NeighborhoodFactory;
        }

    }
}
