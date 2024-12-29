using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Extensions;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Core;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class AlgorithmsView : FrameView
    {
        public AlgorithmsView([KeyFilter(KeyFilters.Views)] IMessenger messenger,
            IRequireAlgorithmNameViewModel viewModel)
        {
            Initialize();
            var algos = Enum.GetValues(typeof(Algorithms))
                .Cast<Algorithms>()
                .ToArray();
            algorithms.SetSource(algos.Select(x => x.ToStringRepresentation()).ToArray());
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
