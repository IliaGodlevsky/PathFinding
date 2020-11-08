using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using WinFormsVersion.Extensions;
using WinFormsVersion.Forms;
using WinFormsVersion.Model;
using WinFormsVersion.View;
using WinFormsVersion.EventHolder;
using GraphLib.GraphField;
using WinFormsVersion.Vertex;
using System.Linq;
using GraphLib.Graphs;
using GraphViewModel;
using GraphLib.Extensions;
using Common;

namespace WinFormsVersion.ViewModel
{
    internal class MainWindowViewModel : MainModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string graphParametres;
        public override string GraphParametres
        {
            get { return graphParametres; }
            set { graphParametres = value; OnPropertyChanged(); }
        }

        private string statistics;
        public override string PathFindingStatistics
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
                int size = VertexParametres.SizeBetweenVertices;
                var field = graphField as WinFormsGraphField;

                MainWindow.Controls.RemoveBy(ctrl => ctrl.IsGraphField());

                field.Size = new Size((Graph as Graph2d).Width * size, (Graph as Graph2d).Length * size);
                MainWindow.Controls.Add(field);
            }
        }

        public Form Window { get; set; }
        public MainWindow MainWindow { get; set; }

        public MainWindowViewModel() : base()
        {
            VertexEventHolder = new WinFormsVertexEventHolder();
            graphField = new WinFormsGraphField();
            FieldFactory = new WinFormsGraphFieldFactory();
            DtoConverter = (dto) => new WinFormsVertex(dto);
        }


        public override void FindPath()
        {
            try
            {
                if (!CanStartPathFinding())
                    return;

                var model = new PathFindingViewModel(this);
                var form = new PathFindingWindow(model);

                PrepareWindow(form);
            }
            catch(Exception ex)
            {
                logger.Log(ex);
            }
        }

        public override void CreateNewGraph()
        {
            try
            {
                var model = new GraphCreatingViewModel(this);
                var form = new GraphCreatingWIndow(model);

                PrepareWindow(form);
            }
            catch(Exception ex)
            {
                logger.Log(ex);
            }
        }

        public void Dispose()
        {
            OnDispose();
        }

        public void SaveGraph(object sender, EventArgs e)
        {
            try
            {
                if (!Graph.IsDefault)
                    base.SaveGraph();
            }
            catch(Exception ex)
            {
                logger.Log(ex);
            }
        }

        public void LoadGraph(object sender, EventArgs e)
        {
            try
            {
                base.LoadGraph();
                Window?.Close();
            }
            catch(Exception ex)
            {
                logger.Log(ex);
            }
        }

        public void ClearGraph(object sender, EventArgs e)
        {
            try
            {
                base.ClearGraph();
            }
            catch(Exception ex)
            {
                logger.Log(ex);
            }
        }

        public void MakeWeighted(object sender, EventArgs e)
        {
            try
            {
                Graph.ToWeighted();
            }
            catch(Exception ex)
            {
                logger.Log(ex);
            }
            
        }

        public void MakeUnweighted(object sender, EventArgs e)
        {
            try
            {
                Graph.ToUnweighted();
            }
            catch (Exception ex)
            {
                logger.Log(ex);
            }           
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
            Window.StartPosition = FormStartPosition.CenterScreen;
            Window.Show();
        }

        private bool CanStartPathFinding()
        {
            return !Graph.Start.IsDefault
                && !Graph.End.IsDefault
                && Graph.Any() 
                && !Graph.Start.IsVisited;
        }

        protected override string GetSavingPath()
        {
            var save = new SaveFileDialog();
            return save.ShowDialog() == DialogResult.OK ? save.FileName : string.Empty;
        }

        protected override string GetLoadingPath()
        {
            var open = new OpenFileDialog();
            return open.ShowDialog() == DialogResult.OK ? open.FileName : string.Empty;
        }
    }
}
