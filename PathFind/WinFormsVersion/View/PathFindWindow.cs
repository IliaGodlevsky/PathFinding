using GraphLibrary.Enums.AlgorithmEnum;
using GraphLibrary.Extensions;
using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsVersion.ViewModel;

namespace WinFormsVersion.View
{
    public partial class PathFindWindow : Form
    {
        public PathFindViewModel Model { get; set; }
        public PathFindWindow(PathFindViewModel model)
        {
            InitializeComponent();
            Model = model;

            okButton.Click += Model.PathFind;
            cancelButton.Click += Model.CancelPathFind;

            var dataSource = Enum.GetValues(typeof(Algorithms)).Cast<Algorithms>().
                    Select(algo => new { Name = algo.GetDescription(), Value = algo }).ToList();
            algorithmListBox.DataSource = dataSource;
            var type = dataSource.First().GetType();
            var properties = type.GetProperties();

            algorithmListBox.DisplayMember = properties.First().Name;
            algorithmListBox.ValueMember = properties.Last().Name;

            algorithmListBox.SelectedIndexChanged += SelectItem;

            //var bindingAlgorithm = new Binding("SelectedItem.Value", Model, "Algorithm");
            //algorithmListBox.DataBindings.Add(bindingAlgorithm);
        }

        private void SelectItem(object sender, EventArgs e)
        {
            Model.Algorithm = (Algorithms)sender;
        }
    }
}
