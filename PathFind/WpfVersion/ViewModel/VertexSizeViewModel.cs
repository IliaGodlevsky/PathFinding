using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Globals;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.ViewModel.Interface;
using System;
using System.Linq;
using System.Windows.Controls;
using WpfVersion.Infrastructure;
using WpfVersion.Model;
using WpfVersion.Model.Vertex;

namespace WpfVersion.ViewModel
{
    internal class VertexSizeViewModel : IModel
    {       
        public int Size { get; set; }
        public MainWindowViewModel Model { get; set; }
        public RelayCommand ExecuteChangeVertexSize { get; }
        public RelayCommand ExecuteCancel { get; }
        public VertexSizeViewModel(MainWindowViewModel model)
        {
            Model = model;

            ExecuteChangeVertexSize = new RelayCommand(
                ChangeVertexSize, obj => true);
            ExecuteCancel = new RelayCommand(
                obj => Model.Window.Close(), 
                obj => true);

            Size = Convert.ToInt32((Model.Graph.First() as WpfVertex).Width);
        }

        private void ChangeVertexSize(object param)
        {
            VertexSize.VERTEX_SIZE = Size;
            VertexSize.SIZE_BETWEEN_VERTICES = Size + 1;

            IVertex ChangeSize(IVertex vertex)
            {
                var temp = vertex as WpfVertex;
                temp.Width  = VertexSize.VERTEX_SIZE;
                temp.Height = VertexSize.VERTEX_SIZE;
                temp.FontSize = Size * VertexSize.TEXT_TO_SIZE_RATIO;
                return temp;
            }

            Model.Graph.Array.Apply(ChangeSize);

            (Model.GraphField as Canvas).Children.Clear();
            var fieldFactory = new WpfGraphFieldFactory();
            Model.GraphField = fieldFactory.GetGraphField(Model.Graph);

            WindowAdjust.Adjust(Model.Graph);
            Model.Window.Close();
        }
    }
}
