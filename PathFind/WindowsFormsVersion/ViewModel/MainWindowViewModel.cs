using Common;
using Common.Interfaces;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Graphs;
using GraphLib.Interface;
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
                int size = Convert.ToInt32(ConfigurationManager.AppSettings["vertexSize"]) + 1;
                var field = graphField as WinFormsGraphField;

                MainWindow.Controls.RemoveBy(ctrl => ctrl.IsGraphField());

                field.Size = new Size((Graph as Graph2D).Width * size, (Graph as Graph2D).Length * size);
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

        public void Dispose()
        {
            OnDispose();
        }

        public void SaveGraph(object sender, EventArgs e)
        {
            if (!Graph.IsDefault)
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

        protected virtual void OnDispose()
        {
            return;
        }

        private void PrepareWindow(IViewModel model, Form window)
        {
            model.OnWindowClosed += (sender, args) => window.Close();
            window.StartPosition = FormStartPosition.CenterScreen;
            window.Show();
        }

        private bool CanStartPathFinding()
        {
            return EndPoints.HasEndPointsSet();
        }
    }
}
