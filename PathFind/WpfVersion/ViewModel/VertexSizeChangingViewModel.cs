using Common;
using GraphLib.Vertex.Interface;
using GraphViewModel.Interfaces;
using System;
using System.Linq;
using System.Windows.Controls;
using WpfVersion.Infrastructure;
using WpfVersion.Model;
using WpfVersion.Model.Vertex;

namespace WpfVersion.ViewModel
{
    internal class VertexSizeChangingViewModel : IModel
    {       
        public int Size { get; set; }
        public MainWindowViewModel Model { get; set; }
        public RelayCommand ExecuteChangeVertexSize { get; }
        public RelayCommand ExecuteCancel { get; }
        public VertexSizeChangingViewModel(MainWindowViewModel model)
        {
            Model = model;
            ExecuteChangeVertexSize = new RelayCommand(ChangeVertexSize, obj => true);
            ExecuteCancel = new RelayCommand(obj => Model.Window.Close(), obj => true);
            if (Model.Graph.Size > 0)
            {
                Size = Convert.ToInt32((Model.Graph.First() as WpfVertex).Width);
            }
        }

        private void ChangeVertexSize(object param)
        {
            VertexParametres.VertexSize = Size;

            void ChangeSize(IVertex vertex)
            {
                var temp = vertex as WpfVertex;
                temp.Width = VertexParametres.VertexSize;
                temp.Height = VertexParametres.VertexSize;
                temp.FontSize = Size * VertexParametres.TextToSizeRatio;
            }

            foreach (var vertex in Model.Graph)
            {
                ChangeSize(vertex);
            }

            (Model.GraphField as Canvas).Children.Clear();
            var fieldFactory = new WpfGraphFieldFactory();
            Model.GraphField = fieldFactory.CreateGraphField(Model.Graph);

            Model.Window.Close();
        }
    }
}
