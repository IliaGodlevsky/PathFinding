using Common;
using Common.Extensions;
using Common.Interfaces;
using GraphLib.Vertex.Interface;
using GraphViewModel.Interfaces;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

        public ICommand ExecuteChangeVertexSizeCommand { get; }
        public ICommand ExecuteCancelCommand { get; }

        public VertexSizeChangingViewModel(MainWindowViewModel model)
        {
            Model = model;

            ExecuteChangeVertexSizeCommand = new RelayCommand(ExecuteChangeVerticesSizeCommand);
            ExecuteCancelCommand = new RelayCommand(obj => CloseWindow());

            if (Model.Graph.Any())
            {
                VerticesSize = GetSampleSizeToChange();
            }
        }

        private int GetSampleSizeToChange()
        {
            var randomVertex = Model.Graph.GetRandomElement() as WpfVertex;
            return Convert.ToInt32(randomVertex.Width);
        }

        private void CloseWindow()
        {
            OnWindowClosed?.Invoke(this, new EventArgs());
        }

        private void ChangeSize(IVertex vertex)
        {
            var temp = vertex as WpfVertex;
            Application.Current.Dispatcher.Invoke(() =>
            {
                temp.Width = VerticesSize;
                temp.Height = VerticesSize;
                temp.FontSize = VerticesSize * VertexParametres.TextToSizeRatio;
            });
        }

        private void CreateNewGraphField()
        {
            (Model.GraphField as Canvas).Children.Clear();
            var fieldFactory = new WpfGraphFieldFactory();
            Model.GraphField = fieldFactory.CreateGraphField(Model.Graph);
        }

        private void ExecuteChangeVerticesSizeCommand(object param)
        {
            VertexParametres.VertexSize = VerticesSize;
            Model.Graph.ForEach(ChangeSize);
            CreateNewGraphField();
            CloseWindow();
        }
    }
}
