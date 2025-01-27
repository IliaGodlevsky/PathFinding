using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.View;
using Pathfinding.App.Console.ViewModel.Interface;
using Pathfinding.Domain.Core;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class AlgorithmsView : FrameView
    {
        public AlgorithmsView([KeyFilter(KeyFilters.Views)] IMessenger messenger,
            IRequireAlgorithmNameViewModel viewModel)
        {
            Initialize();
            var algos = Enum.GetValues(typeof(Algorithms))
                .Cast<Algorithms>()
                .OrderBy(x => x.GetOrder())
                .ToArray();
            var source = algos
                .Select(x => x.ToStringRepresentation())
                .ToArray();
            algorithms.SetSource(source);
            algorithms.Events().SelectedItemChanged
                .Where(x => x.Item > -1)
                .Select(x => algos[x.Item])
                .Do(algorithm =>
                {
                    // Don't change order of the messages
                    switch (algorithm)
                    {
                        case Algorithms.AStar:
                        case Algorithms.BidirectAStar:
                        case Algorithms.AStarGreedy:
                            messenger.Send(new OpenStepRuleViewMessage());
                            messenger.Send(new OpenAlgorithmsPopulateViewMessage());
                            messenger.Send(new OpenHeuristicsViewMessage());
                            break;
                        case Algorithms.Dijkstra:
                        case Algorithms.BidirectDijkstra:
                        case Algorithms.CostGreedy:
                            messenger.Send(new OpenStepRuleViewMessage());
                            messenger.Send(new CloseAlgorithmsPopulateViewMessage());
                            messenger.Send(new CloseHeuristicsViewMessage());
                            break;
                        case Algorithms.Lee:
                        case Algorithms.BidirectLee:
                        case Algorithms.DepthFirst:
                        case Algorithms.Snake:
                            messenger.Send(new CloseStepRulesViewMessage());
                            messenger.Send(new CloseAlgorithmsPopulateViewMessage());
                            messenger.Send(new CloseHeuristicsViewMessage());
                            break;
                        case Algorithms.DistanceFirst:
                        case Algorithms.AStarLee:
                            messenger.Send(new CloseStepRulesViewMessage());
                            messenger.Send(new CloseAlgorithmsPopulateViewMessage());
                            messenger.Send(new OpenHeuristicsViewMessage());
                            break;
                    }
                })
                .BindTo(viewModel, x => x.Algorithm);
        }
    }
}
