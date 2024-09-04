using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Serialization;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using static Terminal.Gui.View;

namespace Pathfinding.ConsoleApp.ViewModel.ButtonsFrameViewModels
{
    internal sealed class LoadGraphButtonModel : BaseViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<VertexModel> service;
        private readonly ILog logger;
        private readonly ISerializer<PathfindingHistorySerializationModel> serializer;

        private string filePath;
        public string FilePath
        {
            get => filePath;
            set => this.RaiseAndSetIfChanged(ref filePath, value);
        }

        public ReactiveCommand<MouseEventArgs, Unit> LoadGraphCommand { get; }

        public LoadGraphButtonModel([KeyFilter(KeyFilters.ViewModels)]IMessenger messenger,
            ISerializer<PathfindingHistorySerializationModel> serializer,
            IRequestService<VertexModel> service,
            ILog logger)
        {
            this.messenger = messenger;
            this.serializer = serializer;
            this.service = service;
            this.logger = logger;
            LoadGraphCommand = ReactiveCommand.CreateFromTask<MouseEventArgs>(LoadGraph);
        }

        private async Task LoadGraph(MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(FilePath))
            {
                await ExecuteSafe(async () =>
                {
                    var graph = await serializer.DeserializeFromFileAsync(FilePath);
                    var result = await service.CreatePathfindingHistoryAsync(graph);
                    messenger.Send(new GraphCreatedMessage(result.Graph));
                }, logger.Error);
            }
        }
    }
}
