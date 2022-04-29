using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using WindowsFormsVersion.DependencyInjection;
using WindowsFormsVersion.Enums;
using WindowsFormsVersion.EventArguments;
using WindowsFormsVersion.EventHandlers;
using WindowsFormsVersion.Extensions;
using WindowsFormsVersion.Forms;
using WindowsFormsVersion.Messeges;
using WindowsFormsVersion.View;

namespace WindowsFormsVersion.ViewModel
{
    internal class MainWindowViewModel : MainModel, IMainModel, INotifyPropertyChanged, IDisposable
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

        public MainWindowViewModel(IGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder, IGraphSerializationModule serializationModule, BaseEndPoints endPoints, ILog log)
            : base(fieldFactory, eventHolder, serializationModule, endPoints, log)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<AlgorithmStatusMessage>(this, MessageTokens.MainModel, SetAlgorithmStatus);
            messenger.Register<UpdateStatisticsMessage>(this, MessageTokens.MainModel, SetStatisticsMessage);
            messenger.Register<GraphCreatedMessage>(this, MessageTokens.MainModel, SetGraph);
        }

        private void SetAlgorithmStatus(AlgorithmStatusMessage message)
        {
            IsPathfindingStarted = message.IsAlgorithmStarted;
        }

        private void SetStatisticsMessage(UpdateStatisticsMessage message)
        {
            PathFindingStatistics = message.Statistics;
        }

        public override void FindPath()
        {
            if (CanStartPathFinding())
            {
                var form = DI.Container.Resolve<PathFindingWindow>();
                form.Show();
            }
        }

        public override void CreateNewGraph()
        {
            if (!IsPathfindingStarted)
            {
                var form = DI.Container.Resolve<GraphCreatingWindow>();
                form.Show();
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

        public void StartPathFind(object sender, EventArgs e)
        {
            FindPath();
        }

        public void CreateNewGraph(object sender, EventArgs e)
        {
            CreateNewGraph();
        }

        private void SetGraph(GraphCreatedMessage message)
        {
            ConnectNewGraph(message.Graph);
        }

        private bool CanStartPathFinding()
        {
            return !endPoints.HasIsolators() && !IsPathfindingStarted;
        }

        public void Dispose()
        {
            messenger.Unregister(this);
        }

        private readonly IMessenger messenger;
    }
}