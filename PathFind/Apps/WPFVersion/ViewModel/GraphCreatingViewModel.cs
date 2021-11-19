using Common.Interface;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Extensions;
using GraphLib.Interfaces.Factories;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using ValueRange.Extensions;
using WPFVersion.Infrastructure;
using WPFVersion.Messages;

namespace WPFVersion.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel, IModel, IViewModel
    {
        public event EventHandler WindowClosed;

        public ICommand ConfirmCreateGraphCommand { get; }
        public ICommand CancelCreateGraphCommand { get; }

        public GraphCreatingViewModel(ILog log, IEnumerable<IGraphAssemble> graphAssembles)
            : base(log, graphAssembles)
        {
            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand,
                CanExecuteConfirmCreateGraphCommand);
            CancelCreateGraphCommand = new RelayCommand(ExecuteCloseWindowCommand);
        }

        public override async void CreateGraph()
        {
            try
            {
                var graph = await SelectedGraphAssemble.AssembleGraphAsync(ObstaclePercent, GraphParametres);
                var message = new GraphCreatedMessage(graph);
                Messenger.Default.Send(message, MessageTokens.MainModel);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void ExecuteConfirmCreateGraphCommand(object param)
        {
            CreateGraph();
            ExecuteCloseWindowCommand(null);
        }

        private void ExecuteCloseWindowCommand(object param)
        {
            WindowClosed?.Invoke(this, EventArgs.Empty);
            WindowClosed = null;
        }

        private bool CanExecuteConfirmCreateGraphCommand(object sender)
        {
            return SelectedGraphAssemble != null
                && Constants.GraphWidthValueRange.Contains(Width)
                && Constants.GraphLengthValueRange.Contains(Length);
        }
    }
}
