using Common.Interface;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Extensions;
using GraphLib.Interfaces.Factories;
using GraphLib.ViewModel;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ValueRange.Extensions;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Messages;

namespace WPFVersion3D.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel, IViewModel
    {
        public event EventHandler WindowClosed;

        public int Height { get; set; }

        public ICommand ConfirmCreateGraphCommand { get; }
        public ICommand CancelCreateGraphCommand { get; }

        public GraphCreatingViewModel(ILog log, IEnumerable<IGraphAssemble> graphAssembles)
            : base(log, graphAssembles)
        {
            SelectedGraphAssemble = graphAssembles.FirstOrDefault();
            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand,
                CanExecuteConfirmCreateGraphCommand);
            CancelCreateGraphCommand = new RelayCommand(obj => CloseWindow());
        }

        protected override int[] GraphParametres => new[] { Width, Length, Height };

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
            CloseWindow();
        }

        private void CloseWindow()
        {
            WindowClosed?.Invoke(this, EventArgs.Empty);
            WindowClosed = null;
        }

        private bool CanExecuteConfirmCreateGraphCommand(object sender)
        {
            return SelectedGraphAssemble != null
                && Constants.GraphWidthValueRange.Contains(Width)
                && Constants.GraphLengthValueRange.Contains(Length)
                && Constants.GraphHeightValueRange.Contains(Height);
        }
    }
}
