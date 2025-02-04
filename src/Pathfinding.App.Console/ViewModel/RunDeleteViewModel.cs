﻿using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.ViewModel;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.ViewModel.Interface;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using ReactiveUI;
using System.Reactive;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class RunDeleteViewModel : BaseViewModel, IRunDeleteViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<GraphVertexModel> service;
        private readonly ILog logger;

        private int[] selectedRunsIds = [];
        public int[] SelectedRunsIds
        {
            get => selectedRunsIds;
            set => this.RaiseAndSetIfChanged(ref selectedRunsIds, value);
        }

        private int ActivatedGraph { get; set; } = 0;

        public ReactiveCommand<Unit, Unit> DeleteRunsCommand { get; }

        public RunDeleteViewModel(
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IRequestService<GraphVertexModel> service,
            ILog logger)
        {
            this.messenger = messenger;
            this.service = service;
            this.logger = logger;
            messenger.Register<RunSelectedMessage>(this, OnRunsSelected);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphsDeleted);
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
            DeleteRunsCommand = ReactiveCommand.CreateFromTask(DeleteRuns, CanDelete());
        }

        private IObservable<bool> CanDelete()
        {
            return this.WhenAnyValue(x => x.SelectedRunsIds,
                ids => ids.Length > 0);
        }

        private async Task DeleteRuns()
        {
            await ExecuteSafe(async () =>
            {
                var isDeleted = await service.DeleteRunsAsync(SelectedRunsIds).ConfigureAwait(false);
                if (isDeleted)
                {
                    var runs = SelectedRunsIds.ToArray();
                    SelectedRunsIds = [];
                    messenger.Send(new RunsDeletedMessage(runs));
                }
            }, logger.Error).ConfigureAwait(false);
        }

        private void OnRunsSelected(object recipient, RunSelectedMessage msg)
        {
            SelectedRunsIds = msg.SelectedRuns.Select(x => x.Id).ToArray();
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            ActivatedGraph = msg.Graph.Id;
        }

        private void OnGraphsDeleted(object recipient, GraphsDeletedMessage msg) 
        {
            if (msg.GraphIds.Contains(ActivatedGraph))
            {
                ActivatedGraph = 0;
                SelectedRunsIds = [];
            }
        }
    }
}
