using Algorithm.Realizations;
using Common.Interface;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations;
using GraphLib.Realizations.Graphs;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Logging.Loggers;
using NullObject.Extensions;
using System;
using System.ComponentModel;
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
            get => graphParametres;
            set { graphParametres = value; OnPropertyChanged(); }
        }

        private string statistics;
        public override string PathFindingStatistics
        {
            get => statistics;
            set { statistics = value; OnPropertyChanged(); }
        }

        private IGraphField graphField;
        public override IGraphField GraphField
        {
            get => graphField;
            set
            {
                graphField = value;
                if (Graph is Graph2D graph2D && graphField is WinFormsGraphField field)
                {
                    int width = (graph2D.Width + Constants.VertexSize) * Constants.VertexSize;
                    int height = (graph2D.Length + Constants.VertexSize) * Constants.VertexSize;
                    field.Size = new Size(width, height);
                    MainWindow.Controls.RemoveBy(ctrl => ctrl.IsGraphField());
                    MainWindow.Controls.Add(field);
                }
            }
        }

        public MainWindow MainWindow { get; set; }

        public MainWindowViewModel(BaseGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder,
            ISaveLoadGraph saveLoad,
            ConcreteGraphAssembleClasses graphFactories,
            ConcreteAssembleAlgorithmClasses assembleClasses,
            Logs log)
            : base(fieldFactory, eventHolder, saveLoad,
                  graphFactories, assembleClasses, log)
        {

        }

        public override void FindPath()
        {
            if (CanStartPathFinding())
            {
                try
                {
                    assembleClasses.LoadClasses();
                    var model = new PathFindingViewModel(log, assembleClasses, this, EndPoints);
                    var form = new PathFindingWindow(model);
                    PrepareWindow(model, form);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
        }

        public override void CreateNewGraph()
        {
            try
            {
                var model = new GraphCreatingViewModel(log, this, graphFactories);
                var form = new GraphCreatingWindow(model);
                PrepareWindow(model, form);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public void SaveGraph(object sender, EventArgs e)
        {
            if (!Graph.IsNullObject())
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
    }
}
