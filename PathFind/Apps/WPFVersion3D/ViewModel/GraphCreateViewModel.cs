using Common.Interface;
using GraphLib.Interfaces.Factories;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
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

        public GraphCreatingViewModel(IMainModel model, IGraphAssemble graphFactory) : base(model, graphFactory)
        {
            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand);
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
    }
}
