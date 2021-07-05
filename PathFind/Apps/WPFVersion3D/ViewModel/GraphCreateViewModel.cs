using Common.Interface;
using GraphLib.Interfaces.Factories;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel
{
    internal class GraphCreatingViewModel : GraphCreatingModel, IViewModel
    {
        public event EventHandler OnWindowClosed;

        public int Height { get; set; }

        public ICommand ConfirmCreateGraphCommand { get; }
        public ICommand CancelCreateGraphCommand { get; }

        public GraphCreatingViewModel(ILog log, IMainModel model,
            IEnumerable<IGraphAssemble> graphAssembles)
            : base(log, model, graphAssembles)
        {
            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand,
                CanExecuteConfirmCreateGraphCommand);
            CancelCreateGraphCommand = new RelayCommand(obj => CloseWindow());
        }

        protected override int[] GraphParametres => new[] { Width, Length, Height };

        private void ExecuteConfirmCreateGraphCommand(object param)
        {
            CreateGraph();

            var field = model.GraphField as GraphField3D;

            field?.CenterGraph();

            CloseWindow();
        }

        private void CloseWindow()
        {
            OnWindowClosed?.Invoke(this, EventArgs.Empty);
            OnWindowClosed = null;
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
