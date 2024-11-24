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
using ReactiveUI;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class AlgorithmsView : FrameView
    {
        public AlgorithmsView([KeyFilter(KeyFilters.Views)]IMessenger messenger,
            IRequireAlgorithmNameViewModel viewModel)
        {
            Initialize();
            var source = new[] 
            { 
                AlgorithmNames.Dijkstra, 
                AlgorithmNames.BidirectDijkstra, 
                AlgorithmNames.AStar,
                AlgorithmNames.BidirectAStar,
                AlgorithmNames.Lee,
                AlgorithmNames.BidirectLee,
                AlgorithmNames.AStarLee,
                AlgorithmNames.DistanceFirst,
                AlgorithmNames.CostGreedy,
                AlgorithmNames.AStarGreedy,
                AlgorithmNames.DepthFirst,
                AlgorithmNames.Snake
            };
            algorithms.SetSource(source);
            algorithms.Events().SelectedItemChanged
                .Where(x => x.Item > -1)
                .Select(x => x.Value.ToString())
                .Do(algorithmName =>
                {
                    switch (algorithmName)
                    {
                        case AlgorithmNames.AStar:
                        case AlgorithmNames.BidirectAStar:
                        case AlgorithmNames.AStarGreedy:
                            messenger.Send(new OpenStepRuleViewMessage());
                            messenger.Send(new OpenHeuristicsViewMessage());
                            break;
                        case AlgorithmNames.Dijkstra:
                        case AlgorithmNames.BidirectDijkstra:
                        case AlgorithmNames.CostGreedy:
                            messenger.Send(new OpenStepRuleViewMessage());
                            messenger.Send(new CloseHeuristicsViewMessage());
                            break;
                        case AlgorithmNames.Lee:
                        case AlgorithmNames.BidirectLee:
                        case AlgorithmNames.DepthFirst:
                        case AlgorithmNames.Snake:
                            messenger.Send(new CloseStepRulesViewMessage());
                            messenger.Send(new CloseHeuristicsViewMessage());
                            break;
                        case AlgorithmNames.DistanceFirst:
                        case AlgorithmNames.AStarLee:
                            messenger.Send(new CloseStepRulesViewMessage());
                            messenger.Send(new OpenHeuristicsViewMessage());
                            break;
                    }
                })
                .BindTo(viewModel, x => x.AlgorithmName);
        }
    }
}
