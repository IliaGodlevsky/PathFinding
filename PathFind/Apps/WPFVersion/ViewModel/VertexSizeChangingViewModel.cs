using Common.Extensions;
using Common.Interface;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphViewModel.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;
using WPFVersion.Infrastructure;
using WPFVersion.Model;

namespace WPFVersion.ViewModel
{
    internal class VertexSizeChangingViewModel : IModel, IViewModel
    {
        public event EventHandler OnWindowClosed;

        public int VerticesSize { get; set; }

        public MainWindowViewModel Model { get; set; }

        public ICommand ChangeVertexSizeCommand { get; }
        public ICommand CancelCommand { get; }

        public VertexSizeChangingViewModel(MainWindowViewModel model, BaseGraphFieldFactory fieldFactory)
        {
            Model = model;
            this.fieldFactory = fieldFactory;

            ChangeVertexSizeCommand = new RelayCommand(ExecuteChangeVerticesSizeCommand);
            CancelCommand = new RelayCommand(ExecuteCancelCommand);

            if (!Model.Graph.IsEmpty())
            {
                VerticesSize = GetSampleSizeToChange();
            }
        }

        private int GetSampleSizeToChange()
        {
            var randomVertex = Model.Graph.Vertices.GetRandomElementOrDefault();
            if (randomVertex is Vertex vertex)
            {
                return Convert.ToInt32(vertex.Width);
            }
            return Constants.VertexSize;
        }

        private void ChangeSize(IVertex vertex)
        {
            if (vertex is Vertex temp)
            {
                Application.Current?.Dispatcher?.Invoke(() =>
                {
                    temp.Width = VerticesSize;
                    temp.Height = VerticesSize;
                    temp.FontSize = VerticesSize * Constants.TextToSizeRatio;
                });
            }
        }

        private void ExecuteChangeVerticesSizeCommand(object param)
        {
            Model.Graph.Vertices.ForEach(ChangeSize);
            Model.GraphField.Clear();
            Model.GraphField = fieldFactory.CreateGraphField(Model.Graph);
            OnWindowClosed?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteCancelCommand(object param)
        {
            OnWindowClosed?.Invoke(this, EventArgs.Empty);
        }

        private readonly BaseGraphFieldFactory fieldFactory;
    }
}
