﻿using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
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
    internal sealed class GraphDeletionViewModel : BaseViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<GraphVertexModel> service;
        private readonly ILog logger;

        private int[] graphIds = Array.Empty<int>();
        public int[] GraphIds
        {
            get => graphIds;
            set => this.RaiseAndSetIfChanged(ref graphIds, value);
        }

        public ReactiveCommand<MouseEventArgs, Unit> DeleteCommand { get; }

        public GraphDeletionViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IRequestService<GraphVertexModel> service,
            ILog logger)
        {
            DeleteCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(DeleteGraph, CanDelete());
            this.messenger = messenger;
            this.service = service;
            this.logger = logger;
            messenger.Register<GraphSelectedMessage>(this, OnGraphSelected);
        }

        private IObservable<bool> CanDelete()
        {
            return this.WhenAnyValue(x => x.GraphIds,
                (ids) => ids.Length > 0);
        }

        private async Task DeleteGraph(MouseEventArgs e)
        {
            await ExecuteSafe(async () =>
            {
                var isDeleted = await Task.Run(() => service.DeleteGraphsAsync(graphIds))
                    .ConfigureAwait(false);
                if (isDeleted)
                {
                    messenger.Send(new GraphsDeletedMessage(graphIds.ToArray()));
                }
            }, logger.Error).ConfigureAwait(false);
        }

        private void OnGraphSelected(object recipient, GraphSelectedMessage msg)
        {
            GraphIds = msg.GraphIds;
        }
    }
}
