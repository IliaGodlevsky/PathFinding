using Algorithm.Realizations;
using Common;
using Common.Extensions;
using Common.Interfaces;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.Realizations;
using GraphLib.Serialization.Interfaces;
using GraphViewModel;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using WindowsFormsVersion.Extensions;
using WindowsFormsVersion.Forms;
using WindowsFormsVersion.View;

namespace WindowsFormsVersion.ViewModel
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
                var field = graphField as WinFormsGraphField;
                var graph = Graph as Graph2D;
                int width = graph.Width * Constants.VertexSize;
                int height = graph.Length * Constants.VertexSize;
                field.Size = new Size(width, height);
                MainWindow.Controls.RemoveBy(ctrl => ctrl.IsGraphField());
                MainWindow.Controls.Add(field);
            }
        }

        public MainWindow MainWindow { get; set; }

        public MainWindowViewModel(BaseGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder,
            IGraphSerializer graphSerializer,
            IGraphAssembler graphFactory, 
            IPathInput pathInput) : base(fieldFactory, eventHolder, graphSerializer, graphFactory, pathInput)
        {
            graphSerializer.OnExceptionCaught += exception => MessageBox.Show(exception.Message);
            graphFactory.OnExceptionCaught += exception => MessageBox.Show(exception.Message);
        }

        public override void FindPath()
        {
            if (CanStartPathFinding())
            {
                try
                {
                    AlgorithmsFactory.LoadAlgorithms(GetAlgorithmsLoadPath());
                    var model = new PathFindingViewModel(this)
                    {
                        EndPoints = EndPoints
                    };
                    model.OnPathNotFound += message => MessageBox.Show(message);
                    var form = new PathFindingWindow(model);

                    PrepareWindow(model, form);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Error(ex);
                }
            }
        }

        public override void CreateNewGraph()
        {
            try
            {
                var model = new GraphCreatingViewModel(this, graphAssembler);
                var form = new GraphCreatingWindow(model);

                PrepareWindow(model, form);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }

        }

        public void SaveGraph(object sender, EventArgs e)
        {
            if (!Graph.IsDefault())
            {
                base.SaveGraph();
            }
        }

        public void LoadGraph(object sender, EventArgs e)
        {
            base.LoadGraph();
        }

        public void ClearGraph(object sender, EventArgs e)
        {
            base.ClearGraph();
        }

        public void MakeWeighted(object sender, EventArgs e)
        {
            Graph.ToWeighted();
        }

        public void MakeUnweighted(object sender, EventArgs e)
        {
            Graph.ToUnweighted();
        }

        public void StartPathFind(object sender, EventArgs e)
        {
            FindPath();
        }

        public void CreateNewGraph(object sender, EventArgs e)
        {
            CreateNewGraph();
        }

        private void PrepareWindow(IViewModel model, Form window)
        {
            model.OnWindowClosed += (sender, args) => window.Close();
            window.StartPosition = FormStartPosition.CenterScreen;
            window.Show();
        }

        private bool CanStartPathFinding()
        {
            return EndPoints.HasEndPointsSet;
        }

        protected override string GetAlgorithmsLoadPath()
        {
            return ConfigurationManager.AppSettings["pluginsPath"];
        }
    }
}
