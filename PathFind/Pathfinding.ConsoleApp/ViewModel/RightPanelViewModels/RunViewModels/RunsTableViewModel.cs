using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Undefined;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel.RightPanelViewModels.RunViewModels
{
    internal sealed class RunsTableViewModel : BaseViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<VertexModel> service;
        private readonly ILog logger;

        public ObservableCollection<RunModel> Runs { get; } = new();

        private RunModel activated;
        public RunModel Activated
        {
            get => activated;
            set => this.RaiseAndSetIfChanged(ref activated, value);
        }

        private RunModel[] selected;
        public RunModel[] Selected
        {
            get => selected;
            set
            {
                this.RaiseAndSetIfChanged(ref selected, value);
                var runs = selected.Select(x => x.RunId).ToArray();
                messenger.Send(new RunSelectedMessage(runs));
            }
        }

        private int ActivatedGraphId { get; set; }

        public RunsTableViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IRequestService<VertexModel> service,
            ILog logger)
        {
            this.messenger = messenger;
            this.service = service;
            this.logger = logger;
            messenger.Register<RunHistoriesUploadedMessage>(this, OnRunUploaded);
            messenger.Register<RunCreatedMessaged>(this, OnRunCreated);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
            messenger.Register<GraphActivatedMessage>(this, 
                async (r, msg) => await OnGraphActivatedMessage(r, msg));
            messenger.Register<RunsDeletedMessage>(this, OnRunsDeleteMessage);
        }

        private async Task OnGraphActivatedMessage(object recipient, GraphActivatedMessage msg)
        {
            var statistics = await service.ReadRunStatisticsAsync(msg.GraphId);
            var models = statistics.Select(x => GetModel(x)).ToArray();
            ActivatedGraphId = msg.GraphId;
            while (Runs.Count > 0)
            {
                Runs.RemoveAt(0);
            }
            Runs.Add(models);
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            if (msg.GraphIds.Contains(ActivatedGraphId))
            {
                while (Runs.Count > 0)
                {
                    Runs.RemoveAt(0);
                }
                ActivatedGraphId = 0;
            }
        }

        private void OnRunsDeleteMessage(object recipient, RunsDeletedMessage msg)
        {
            var toDelete = Runs.Where(x => msg.RunIds.Contains(x.RunId)).ToArray();
            Runs.Remove(toDelete);
        }

        private void OnRunUploaded(object recipient, RunHistoriesUploadedMessage msg)
        {
            if (msg.Runs.Length > 0)
            {
                var models = msg.Runs
                    .Select(x => x.Statistics)
                    .Select(x => GetModel(x)).ToArray();
                Runs.Add(models);
            }
        }

        private void OnRunCreated(object recipient, RunCreatedMessaged msg)
        {
            Runs.Add(GetModel(msg.Model.Statistics));
        }

        private RunModel GetModel(RunStatisticsModel model)
        {
            return new ()
            {
                RunId = model.AlgorithmRunId,
                Name = model.AlgorithmId,
                Cost = model.Cost ?? 0,
                Steps = model.Steps ?? 0,
                Status = model.ResultStatus,
                StepRule = model.StepRule ?? "",
                Heuristics = model.Heuristics ?? "",
                Spread = model.Spread?.ToString() ?? "",
                Elapsed = model.Elapsed ?? TimeSpan.FromMilliseconds(0),
                Visited = model.Visited ?? 0
            };
        }
    }
}
