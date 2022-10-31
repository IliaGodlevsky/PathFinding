using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Extensions;
using System.Collections.Generic;
using System.Windows.Input;
using WPFVersion.DependencyInjection;
using WPFVersion.Infrastructure;
using WPFVersion.Messages.DataMessages;
using WPFVersion.Model;

namespace WPFVersion.ViewModel.ButtonViewModels
{
    internal class SmoothGraphViewModel
    {
        private readonly IMessenger messenger;
        private readonly IMeanCost meanCost;
        private readonly IVertexCostFactory costFactory;

        private IGraph<Vertex> Graph { get; set; }

        public ISmoothLevel SelectSmoothLevel { get; set; }

        public IEnumerable<ISmoothLevel> Levels => WpfSmoothLevels.Levels;

        public ICommand SmoothCommand { get; }

        public SmoothGraphViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            meanCost = DI.Container.Resolve<IMeanCost>();
            costFactory = DI.Container.Resolve<IVertexCostFactory>();
            SmoothCommand = new RelayCommand(ExecuteSmoothCommand);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        private void ExecuteSmoothCommand(object param)
        {
            Graph?.Smooth(costFactory, meanCost, SelectSmoothLevel.Level);
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Graph;
        }
    }
}
