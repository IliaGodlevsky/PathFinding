using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Serialization;
using Pathfinding.Shared.Extensions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using static Terminal.Gui.View;

namespace Pathfinding.ConsoleApp.ViewModel.ButtonsFrameViewModels
{
    internal sealed class SaveGraphButtonViewModel : BaseViewModel
    {
        private readonly IRequestService<VertexModel> service;
        private readonly ISerializer<List<PathfindingHistorySerializationModel>> serializer;
        private readonly IMessenger messenger;
        private readonly ILog logger;

        private int[] graphIds = Array.Empty<int>();
        public int[] GraphIds
        {
            get => graphIds;
            set => this.RaiseAndSetIfChanged(ref graphIds, value);
        }

        private string filePath;
        public string FilePath 
        {
            get => filePath;
            set => this.RaiseAndSetIfChanged(ref filePath, value); 
        }

        public ReactiveCommand<MouseEventArgs, Unit> SaveGraphCommand { get; }

        public SaveGraphButtonViewModel(IRequestService<VertexModel> service,
            ISerializer<List<PathfindingHistorySerializationModel>> serializer,
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger,
            ILog logger)
        {
            this.service = service;
            this.serializer = serializer;
            this.logger = logger;
            SaveGraphCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(SaveGraph, CanSave());
            messenger.Register<GraphSelectedMessage>(this, OnGraphSelected);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
        }

        private IObservable<bool> CanSave()
        {
            return this.WhenAnyValue(
                x => x.GraphIds,
                graphIds => graphIds.Length > 0);
        }

        private async Task SaveGraph(MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(FilePath))
            {
                await ExecuteSafe(async () =>
                {
                    var graphs = await service.ReadSerializationHistoriesAsync(graphIds)
                        .ConfigureAwait(false);
                    await serializer.SerializeToFileAsync(graphs.ToList(), FilePath)
                        .ConfigureAwait(false);
                    FilePath = string.Empty;
                }, logger.Error);
            }
        }

        private void OnGraphSelected(object recipient, GraphSelectedMessage msg)
        {
            GraphIds = msg.GraphIds;
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            GraphIds = GraphIds.Except(msg.GraphIds).ToArray();
        }
    }
}
