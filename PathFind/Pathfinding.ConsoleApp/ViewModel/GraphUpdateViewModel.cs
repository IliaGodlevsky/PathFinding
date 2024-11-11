using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Core;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using static Terminal.Gui.View;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class GraphUpdateViewModel : BaseViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<GraphVertexModel> service;
        private readonly ILog log;

        private GraphInfoModel[] selectedGraph = Array.Empty<GraphInfoModel>();
        public GraphInfoModel[] SelectedGraphs
        {
            get => selectedGraph;
            set => this.RaiseAndSetIfChanged(ref selectedGraph, value);
        }

        private string smoothLevel;
        public string SmoothLevel
        {
            get => smoothLevel;
            set => this.RaiseAndSetIfChanged(ref smoothLevel, value);
        }

        private string neighborhood;
        public string Neighborhood
        {
            get => neighborhood;
            set => this.RaiseAndSetIfChanged(ref neighborhood, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        private bool isReadOnly;
        public bool IsReadOnly
        {
            get => isReadOnly;
            set => this.RaiseAndSetIfChanged(ref isReadOnly, value);
        }

        public ReactiveCommand<MouseEventArgs, Unit> UpdateGraphCommand { get; }

        public GraphUpdateViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IRequestService<GraphVertexModel> service,
            ILog log)
        {
            this.messenger = messenger;
            this.service = service;
            this.log = log;
            messenger.Register<GraphSelectedMessage>(this, OnGraphSelected);
            messenger.Register<GraphBecameReadOnlyMessage>(this, OnBecameReadOnly);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
            UpdateGraphCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(ExecuteUpdate, CanExecute());
        }

        private IObservable<bool> CanExecute()
        {
            return this.WhenAnyValue(x => x.SelectedGraphs,
                x => x.Name,
                x => x.SmoothLevel,
                x => x.Neighborhood,
                (selected, name, level, neighbor)
                => selected.Length == 1
                    && !string.IsNullOrEmpty(name)
                    && !string.IsNullOrEmpty(level)
                    && !string.IsNullOrEmpty(neighbor));
        }

        private async Task ExecuteUpdate(MouseEventArgs e)
        {
            await ExecuteSafe(async () =>
            {
                var graph = SelectedGraphs[0];
                var info = await service.ReadGraphInfoAsync(graph.Id).ConfigureAwait(false);
                info.Name = Name;
                info.Neighborhood = Neighborhood;
                info.SmoothLevel = SmoothLevel;
                bool updated = await service.UpdateGraphInfoAsync(info).ConfigureAwait(false);
                if (updated)
                {
                    messenger.Send(new GraphUpdatedMessage(info));
                }
            }, log.Error).ConfigureAwait(false);
        }

        private void OnBecameReadOnly(object recipient, GraphBecameReadOnlyMessage msg)
        {
            IsReadOnly = msg.Became;
        }

        private void OnGraphSelected(object recipient, GraphSelectedMessage msg)
        {
            if (msg.Graphs.Length == 1)
            {
                SelectedGraphs = msg.Graphs;
                var graph = SelectedGraphs[0];
                Name = graph.Name;
                SmoothLevel = graph.SmoothLevel;
                Neighborhood = graph.Neighborhood;
                IsReadOnly = graph.Status == GraphStatuses.Readonly;
            }
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            var deleted = SelectedGraphs.Where(x => msg.GraphIds.Contains(x.Id)).ToList();
            SelectedGraphs = SelectedGraphs.Except(deleted).ToArray();
            if (SelectedGraphs.Length == 0)
            {
                Name = string.Empty;
                Neighborhood = string.Empty;
                SmoothLevel = string.Empty;
            }
        }
    }
}
