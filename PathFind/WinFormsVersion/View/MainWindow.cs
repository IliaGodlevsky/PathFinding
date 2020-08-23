using GraphLibrary.Model;
using System.Windows.Forms;
using WinFormsVersion.Model;
using WinFormsVersion.ViewModel;

namespace WinFormsVersion.Forms
{
    public partial class MainWindow : Form
    {
        private MainWindowViewModel mainModel;
        public WinFormsGraphField GraphField
        {
            get { return winFormsGraphField; }
            set { winFormsGraphField = value; }
        }

        public MainWindow()
        {
            InitializeComponent();

            mainModel = new MainWindowViewModel();
            mainModel.MainWindow = this;
            button1.Click += mainModel.CreateNewGraph;
            button2.Click += mainModel.StartPathFind;
        }       
    }
}
