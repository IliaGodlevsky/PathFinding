using System;
using System.Collections.Generic;
using System.Windows.Forms;
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

            var bindingStatistics = new Binding(
                nameof(statistics.Text),
                mainModel,
                nameof(mainModel.PathFindingStatistics));
            statistics.DataBindings.Add(bindingStatistics);

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
    }
}
