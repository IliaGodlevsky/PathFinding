using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;
using System.Linq;
using System.Reactive.Linq;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using Pathfinding.Domain.Core;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using System;
using Pathfinding.ConsoleApp.Extensions;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class AlgorithmsView : FrameView
    {
        public AlgorithmsView([KeyFilter(KeyFilters.Views)]IMessenger messenger,
            IRequireAlgorithmNameViewModel viewModel)
        {
            Initialize();
            var algos = Enum.GetValues(typeof(Algorithms))
                .Cast<Algorithms>()
                .ToDictionary(x => x.ToStringRepresentation());
            var source = algos.Keys.ToList();
            var values = source.Select(x => algos[x]).ToList();
            algorithms.SetSource(source);
            algorithms.Events().SelectedItemChanged
                .Where(x => x.Item > -1)
                .Select(x => values[x.Item])
                .Do(algorithm =>
                {
                    viewModel.Algorithm = algorithm;
                    switch (algorithm)
                    {
                        case Algorithms.AStar:
                        case Algorithms.BidirectAStar:
                        case Algorithms.AStarGreedy:
                            messenger.Send(new OpenStepRuleViewMessage());
                            messenger.Send(new OpenHeuristicsViewMessage());
                            break;
                        case Algorithms.Dijkstra:
                        case Algorithms.BidirectDijkstra:
                        case Algorithms.CostGreedy:
                            messenger.Send(new OpenStepRuleViewMessage());
                            messenger.Send(new CloseHeuristicsViewMessage());
                            break;
                        case Algorithms.Lee:
                        case Algorithms.BidirectLee:
                        case Algorithms.DepthFirst:
                        case Algorithms.Snake:
                            messenger.Send(new CloseStepRulesViewMessage());
                            messenger.Send(new CloseHeuristicsViewMessage());
                            break;
                        case Algorithms.DistanceFirst:
                        case Algorithms.AStarLee:
                            messenger.Send(new CloseStepRulesViewMessage());
                            messenger.Send(new OpenHeuristicsViewMessage());
                            break;
                    }
                })
                .Subscribe();
        }
    }
}
