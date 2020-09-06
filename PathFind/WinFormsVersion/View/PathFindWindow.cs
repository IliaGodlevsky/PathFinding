using GraphLibrary.AlgorithmEnum;
using GraphLibrary.Extensions;
using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsVersion.ViewModel;

namespace WinFormsVersion.View
{
    internal partial class PathFindWindow : Form
    {
        public PathFindViewModel Model { get; set; }
        public PathFindWindow(PathFindViewModel model)
        {
            InitializeComponent();

            Model = model;

            okButton.Click += Model.PathFind;
            cancelButton.Click += Model.CancelPathFind;

            var dataSource = Enum.GetValues(typeof(Algorithms)).
                Cast<Algorithms>().
                Select(algo => new { Name = algo.GetDescription(), Value = algo }).
                ToList();
            algorithmListBox.DataSource = dataSource;

            var type = dataSource.First().GetType();
            var properties = type.GetProperties();

            algorithmListBox.DisplayMember = properties.First().Name;
            algorithmListBox.ValueMember = properties.Last().Name;

            var bindingAlgorithm = new Binding("SelectedValue", Model, "Algorithm");
            algorithmListBox.DataBindings.Add(bindingAlgorithm);

            var bindingDelaySliderToDelayTextBox = new Binding("Value", delayTextBox, "Text",
                true, DataSourceUpdateMode.OnPropertyChanged);
            delaySlider.DataBindings.Add(bindingDelaySliderToDelayTextBox);
            delaySlider.Minimum = 1;
            delaySlider.Maximum = 99;

            var bindingDelatTextBoxToModel = new Binding("Text", Model, "DelayTime", 
                true, DataSourceUpdateMode.OnPropertyChanged);
            delayTextBox.DataBindings.Add(bindingDelatTextBoxToModel);

        }
    }
}
