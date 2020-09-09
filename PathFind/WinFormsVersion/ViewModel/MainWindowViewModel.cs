using GraphLibrary.Common.Constants;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using WinFormsVersion.Extensions;
using WinFormsVersion.Forms;
using WinFormsVersion.GraphLoader;
using WinFormsVersion.Model;
using WinFormsVersion.Resources;
using WinFormsVersion.View;
using WinFormsVersion.EventHolder;
using GraphLibrary.Graphs;
using GraphLibrary.ViewModel;
using GraphLibrary.GraphField;
using GraphLibrary.Vertex;
using GraphLibrary.GraphSerialization.GraphSaver;

namespace WinFormsVersion.ViewModel
{
    internal class MainWindowViewModel : AbstractMainModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

        private Graph graph;
        public override Graph Graph
        {
            get { return graph; }
            set { graph = value; OnPropertyChanged(); }
        }
        public Form Window { get; set; }
        public MainWindow MainWindow { get; set; }

        public MainWindowViewModel()
        {
            GraphParametresFormat = WinFormsVersionResources.ParametresFormat;
            saver = new GraphSaver();
            loader = new WinFormsGraphLoader(VertexSize.SIZE_BETWEEN_VERTICES);
            VertexEventHolder = new WinFormsVertexEventHolder();
            graphField = new WinFormsGraphField();
            graphFieldFiller = new WinFormsGraphFieldFiller();
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

        public void Dispose()
        {
            OnDispose();
        }

        public void SaveGraph(object sender, EventArgs e)
        {
            base.SaveGraph();
        }

        public void LoadGraph(object sender, EventArgs e)
        {
            base.LoadGraph();
            if (Graph == null)
                return;          
            Window?.Close();
            
        }

        public void ClearGraph(object sender, EventArgs e)
        {
            if (graph != null)
                base.ClearGraph();
        }

        public void StartPathFind(object sender, EventArgs e)
        {
            FindPath();
        }

        public void CreateNewGraph(object sender, EventArgs e)
        {
            CreateNewGraph();
        }

        protected virtual void OnDispose()
        {
            return;
        }

        private void PrepareWindow(Form window)
        {
            Window = window;
            Window.Show();
        }

        private bool CanStartPathFinding()
        {
            return graph?.Start != NullVertex.GetInstance()
                && graph?.End != NullVertex.GetInstance()
                && Graph != null;
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
