using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsVersion.EventArguments;
using WindowsFormsVersion.ViewModel;

namespace WindowsFormsVersion.Forms
{
    internal partial class MainWindow : Form
    {
        private readonly MainWindowViewModel mainModel;

        public MainWindow(MainWindowViewModel model)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            mainModel = model;
            mainModel.MainWindow = this;

            statistics.Text = string.Empty;
            statistics.Update();

            var events = new List<EventHandler>()
            {
                mainModel.SaveGraph,
                mainModel.LoadGraph,
                mainModel.CreateNewGraph,
                mainModel.ClearGraph,
                mainModel.StartPathFind,
                mainModel.MakeWeighted,
                mainModel.MakeUnweighted
            };

            weightedButton.GroupID = 1;
            unweightedButton.GroupID = 1;

            mainModel.OnStatisticsChanged += OnStatisticsChanged;

            var bindingParametres = new Binding(
                nameof(parametres.Text),
                mainModel,
                nameof(mainModel.GraphParametres));
            parametres.DataBindings.Add(bindingParametres);

            for (int i = 0; i < menu.Items.Count; i++)
            {
                menu.Items[i].Click += events[i];
            }
        }

        private void OnStatisticsChanged(object sender, StatisticsChangedEventArgs e)
        {
            statistics.Invoke(new Action(() => { statistics.Text = e.Statistics; }));
            statistics.Invoke(new Action(() => { statistics.Update(); }));
        }
    }
}
