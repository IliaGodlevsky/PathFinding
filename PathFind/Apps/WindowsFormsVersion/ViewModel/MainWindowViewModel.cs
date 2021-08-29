using Common.Interface;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Graphs;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using WindowsFormsVersion.EventArguments;
using WindowsFormsVersion.EventHandlers;
using WindowsFormsVersion.Extensions;
using WindowsFormsVersion.Forms;
using WindowsFormsVersion.View;

namespace WindowsFormsVersion.ViewModel
{
    internal class MainWindowViewModel : MainModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event StatisticsChangedEventHandler StatisticsChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsPathfindingStarted { private get; set; }

        private string graphParametres;
        public override string GraphParametres
        {
            get => graphParametres;
            set { graphParametres = value; OnPropertyChanged(); }
        }

        private string statistics;
        public string PathFindingStatistics
        {
            get => statistics;
            set
            {
                statistics = value;
                StatisticsChanged?.Invoke(this, new StatisticsChangedEventArgs(value));
            }
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

        public MainWindowViewModel(IGraphFieldFactory fieldFactory, IVertexEventHolder eventHolder,
            ISaveLoadGraph saveLoad, IEnumerable<IGraphAssemble> graphFactories, BaseEndPoints endPoints, ILog log)
            : base(fieldFactory, eventHolder, saveLoad, graphFactories, endPoints, log)
        {

        }

        public override void FindPath()
        {
            if (CanStartPathFinding())
            {
                try
                {
                    var model = new PathFindingViewModel(log, this, endPoints);
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
            if (!IsPathfindingStarted)
            {
                try
                {
                    var model = new GraphCreatingViewModel(log, this, graphAssembles);
                    var form = new GraphCreatingWindow(model);
                    PrepareWindow(model, form);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
        }

        public void SaveGraph(object sender, EventArgs e)
        {
            if (!Graph.IsNull())
            {
                base.SaveGraph();
            }
        }

        public void LoadGraph(object sender, EventArgs e)
        {
            if (!IsPathfindingStarted)
            {
                base.LoadGraph();
            }
        }

        public void ClearGraph(object sender, EventArgs e)
        {
            if (!IsPathfindingStarted)
            {
                base.ClearGraph();
                PathFindingStatistics = string.Empty;
            }
        }

        public override void ConnectNewGraph(IGraph graph)
        {
            base.ConnectNewGraph(graph);
            PathFindingStatistics = string.Empty;
        }

        public void MakeWeighted(object sender, EventArgs e)
        {
            if (!IsPathfindingStarted)
            {
                Graph.ToWeighted();
            }
        }

        public void MakeUnweighted(object sender, EventArgs e)
        {
            if (!IsPathfindingStarted)
            {
                Graph.ToUnweighted();
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

        private void PrepareWindow(IViewModel model, Form window)
        {
            model.WindowClosed += (sender, args) => window.Close();
            window.StartPosition = FormStartPosition.CenterScreen;
            window.Show();
        }

        private bool CanStartPathFinding()
        {
            return !endPoints.HasIsolators() && !IsPathfindingStarted;
        }
    }
}
