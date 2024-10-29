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
using System.Reactive;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class RunsTableViewModel : BaseViewModel
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<GraphVertexModel> service;
        private readonly ILog logger;

        public ObservableCollection<RunInfoModel> Runs { get; } = new();

        private RunInfoModel[] selected;
        public RunInfoModel[] Selected
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

        public ReactiveCommand<RunInfoModel, Unit> ActivateRunCommand { get; }

        public RunsTableViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            IRequestService<GraphVertexModel> service,
            ILog logger)
        {
            this.messenger = messenger;
            this.service = service;
            this.logger = logger;
            ActivateRunCommand = ReactiveCommand.CreateFromTask<RunInfoModel>(ActivateRun);

            messenger.Register<RunCreatedMessaged>(this, OnRunCreated);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
            messenger.Register<GraphActivatedMessage>(this,
                async (r, msg) => await OnGraphActivatedMessage(r, msg));
            messenger.Register<RunsDeletedMessage>(this, OnRunsDeleteMessage);
        }

        private async Task ActivateRun(RunInfoModel model)
        {
            await ExecuteSafe(async () =>
            {
                var run = await Task.Run(() => service.ReadStatisticAsync(model.RunId))
                    .ConfigureAwait(false);
                messenger.Send(new RunActivatedMessage(run));
            }, logger.Error).ConfigureAwait(false);
        }

        private async Task OnGraphActivatedMessage(object recipient, GraphActivatedMessage msg)
        {
            var statistics = await Task.Run(() => service.ReadStatisticsAsync(msg.Graph.Id))
                .ConfigureAwait(false);
            var models = statistics.Select(GetModel).ToArray();
            ActivatedGraphId = msg.Graph.Id;
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
            if (Runs.Count == 0)
            {
                messenger.Send(new GraphBecameReadOnlyMessage(ActivatedGraphId, false));
            }
        }

        private void OnRunCreated(object recipient, RunCreatedMessaged msg)
        {
            int previousCount = Runs.Count;
            Runs.Add(GetModel(msg.Model));
            if (previousCount == 0)
            {
                messenger.Send(new GraphBecameReadOnlyMessage(ActivatedGraphId, true));
            }
        }

        private RunInfoModel GetModel(RunStatisticsModel model)
        {
            return new()
            {
                RunId = model.Id,
                Name = model.AlgorithmName,
                Cost = model.Cost,
                Steps = model.Steps,
                Status = model.ResultStatus,
                StepRule = model.StepRule,
                Heuristics = model.Heuristics,
                Weight = model.Weight?.ToString(),
                Elapsed = model.Elapsed,
                Visited = model.Visited
            };
        }
    }
}
