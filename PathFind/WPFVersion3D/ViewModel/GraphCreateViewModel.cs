using Common.Interfaces;
using GraphLib.Graphs.Factories.Interfaces;
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

        public GraphCreatingViewModel(IMainModel model,
            IGraphAssembler graphFactory) : base(model, graphFactory)
        {
            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand);
            CancelCreateGraphCommand = new RelayCommand(obj => CloseWindow());
        }

        public override void CreateGraph()
        {
            var graph = graphFactory.AssembleGraph(ObstaclePercent, Width, Length, Height);

            model.ConnectNewGraph(graph);
        }

        private void ExecuteConfirmCreateGraphCommand(object param)
        {
            CreateGraph();

            var field = model.GraphField as GraphField3D;

            field.CenterGraph();

            CloseWindow();
        }

        private void CloseWindow()
        {
            OnWindowClosed?.Invoke(this, new EventArgs());
        }
    }
}
