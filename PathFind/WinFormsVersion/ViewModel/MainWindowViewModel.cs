using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using WinFormsVersion.Extensions;
using WinFormsVersion.Forms;
using WinFormsVersion.Model;
using WinFormsVersion.Resources;
using WinFormsVersion.View;
using WinFormsVersion.EventHolder;
using GraphLibrary.Graphs;
using GraphLibrary.ViewModel;
using GraphLibrary.GraphField;
using GraphLibrary.Vertex;
using WinFormsVersion.Vertex;
using GraphLibrary.Globals;
using GraphLibrary.Graphs.Interface;
using System.Linq;

namespace WinFormsVersion.ViewModel
{
    internal class MainWindowViewModel : AbstractMainModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private string graphParametres;
        public override string GraphParametres
        {
            get { return graphParametres; }
            set { graphParametres = value; OnPropertyChanged(); }
        }

        private string statistics;
        public override string Statistics
        {
            get { return statistics; }
            set { statistics = value; OnPropertyChanged(); }
        }

        private IGraphField graphField;
        public override IGraphField GraphField 
        {
            get => graphField;
            set
            {
                graphField = value;
                int size = VertexSize.SIZE_BETWEEN_VERTICES;
                var field = graphField as WinFormsGraphField;
                MainWindow.Controls.RemoveBy(ctrl => ctrl.IsGraphField());
                field.Size = new Size(Graph.Width * size, Graph.Height * size);
                MainWindow.Controls.Add(field);
            }
        }

        private IGraph graph;
        public override IGraph Graph
        {
            get { return graph; }
            protected set { graph = value; OnPropertyChanged(); }
        }
        public Form Window { get; set; }
        public MainWindow MainWindow { get; set; }

        public MainWindowViewModel() : base()
        {
            GraphParametresFormat = WinFormsVersionResources.ParametresFormat;

            VertexEventHolder = new WinFormsVertexEventHolder();

            graphField = new WinFormsGraphField();
            graphFieldFactory = new WinFormsGraphFieldFactory();

            generator = (dto) => new WinFormsVertex(dto);
        }


        public override void FindPath()
        {
            if (!CanStartPathFinding())
                return;
            var model = new PathFindViewModel(this);
            var form = new PathFindWindow(model);
            PrepareWindow(form);
        }

        public override void CreateNewGraph()
        {
            var model = new CreateGraphViewModel(this);
            var form = new CreateGraphWindow(model);
            PrepareWindow(form);
        }

        public void Dispose() => OnDispose();

        public void SaveGraph(object sender, EventArgs e) => base.SaveGraph();

        public void LoadGraph(object sender, EventArgs e)
        {
            base.LoadGraph();
            Window?.Close();            
        }

        public void ClearGraph(object sender, EventArgs e)
        {
            if (graph != null)
                base.ClearGraph();
        }

        public void StartPathFind(object sender, EventArgs e) => FindPath();

        public void CreateNewGraph(object sender, EventArgs e) => CreateNewGraph();

        protected virtual void OnDispose()
        {
            return;
        }

        private void PrepareWindow(Form window)
        {
            Window = window;
            Window.StartPosition = FormStartPosition.CenterScreen;
            Window.Show();
        }

        private bool CanStartPathFinding()
        {
            return graph.Start != NullVertex.Instance
                && graph.End != NullVertex.Instance
                && Graph.Any() && !graph.Start.IsVisited;
        }

        protected override string GetSavePath()
        {
            var save = new SaveFileDialog();
            return save.ShowDialog() == DialogResult.OK ? save.FileName : string.Empty;
        }

        protected override string GetLoadPath()
        {
            var open = new OpenFileDialog();
            return open.ShowDialog() == DialogResult.OK ? open.FileName : string.Empty;
        }
    }
}
