using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinFormsVersion.Model;
using WinFormsVersion.ViewModel;

namespace WinFormsVersion.Forms
{
    public partial class MainWindow : Form
    {
        private readonly MainWindowViewModel mainModel;
        public WinFormsGraphField GraphField
        {
            get { return winFormsGraphField; }
            set { winFormsGraphField = value; }
        }

        public MainWindow()
        {
            InitializeComponent();

            mainModel = new MainWindowViewModel
            {
                MainWindow = this
            };

            List<EventHandler> events = new List<EventHandler>()
            {
                mainModel.SaveGraph,
                mainModel.LoadGraph,
                mainModel.CreateNewGraph,
                mainModel.ClearGraph,
                mainModel.StartPathFind
            };

            var bindingStatistics = new Binding("Text", mainModel, "Statistics");
            statistics.DataBindings.Add(bindingStatistics);

            var bindingParametres = new Binding("Text", mainModel, "GraphParametres");
            parametres.DataBindings.Add(bindingParametres);

            for (int i = 0; i < menu.Items.Count; i++)
                menu.Items[i].Click += events[i];

        }
    }
}
