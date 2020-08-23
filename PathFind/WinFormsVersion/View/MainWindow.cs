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

            var bindingStatistics = new Binding("Text", mainModel, "Statistics");
            statistics.DataBindings.Add(bindingStatistics);

            var bindingParametres = new Binding("Text", mainModel, "GraphParametres");
            parametres.DataBindings.Add(bindingParametres);

            menu.Items[0].Click += mainModel.SaveGraph;
            menu.Items[1].Click += mainModel.LoadGraph;
            menu.Items[2].Click += mainModel.CreateNewGraph;
            menu.Items[3].Click += mainModel.ClearGraph;
            menu.Items[4].Click += mainModel.StartPathFind;

        }
    }
}
