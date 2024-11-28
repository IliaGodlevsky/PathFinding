﻿using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Extensions;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages;
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

        private SmoothLevels smoothLevel;
        public SmoothLevels SmoothLevel
        {
            get => smoothLevel;
            set => this.RaiseAndSetIfChanged(ref smoothLevel, value);
        }

        private Neighborhoods neighborhood;
        public Neighborhoods Neighborhood
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

        private GraphStatuses status;
        public GraphStatuses Status
        {
            get => status;
            set => this.RaiseAndSetIfChanged(ref status, value);
        }

        public ReactiveCommand<Unit, Unit> UpdateGraphCommand { get; }

        public GraphUpdateViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IRequestService<GraphVertexModel> service,
            ILog log)
        {
            this.messenger = messenger;
            this.service = service;
            this.log = log;
            messenger.Register<GraphSelectedMessage>(this, OnGraphSelected);
            messenger.Register<GraphStateChangedMessage>(this, OnStatusChanged);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
            UpdateGraphCommand = ReactiveCommand.CreateFromTask(ExecuteUpdate, CanExecute());
        }

        private IObservable<bool> CanExecute()
        {
            return this.WhenAnyValue(
                x => x.SelectedGraphs,
                x => x.Name,
                (selected, name) =>
                { 
                    bool canExecute = selected.Length == 1 && !string.IsNullOrEmpty(name);
                    return canExecute;
                });
        }

        private async Task ExecuteUpdate()
        {
            await ExecuteSafe(async () =>
            {
                var graph = SelectedGraphs[0];
                var info = await service.ReadGraphInfoAsync(graph.Id).ConfigureAwait(false);
                info.Name = Name;
                info.Neighborhood = Neighborhood;
                info.SmoothLevel = SmoothLevel;
                await service.UpdateGraphInfoAsync(info).ConfigureAwait(false);
                await messenger.SendAsync(new GraphUpdatedMessage(info), Tokens.GraphTable);
                await messenger.SendAsync(new GraphUpdatedMessage(info), Tokens.AlgorithmUpdate);
                messenger.Send(new GraphUpdatedMessage(info));
            }, log.Error).ConfigureAwait(false);
        }

        private void OnStatusChanged(object recipient, GraphStateChangedMessage msg)
        {
            Status = msg.Status;
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
                Status = graph.Status;
            }
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            SelectedGraphs = SelectedGraphs.Where(x => !msg.GraphIds.Contains(x.Id)).ToArray();
            if (SelectedGraphs.Length == 0)
            {
                Name = string.Empty;
            }
        }
    }
}
