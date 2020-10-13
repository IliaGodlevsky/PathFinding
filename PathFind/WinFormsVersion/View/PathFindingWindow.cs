using GraphLibrary.AlgorithmCreating;
using GraphLibrary.ValueRanges;
using System.Linq;
using System.Windows.Forms;
using WinFormsVersion.ViewModel;

namespace WinFormsVersion.View
{
    internal partial class PathFindingWindow : Form
    {
        public PathFindingViewModel Model { get; set; }
        public PathFindingWindow(PathFindingViewModel model)
        {
            InitializeComponent();

            Model = model;

            okButton.Click += Model.PathFind;
            cancelButton.Click += Model.CancelPathFind;

            var dataSource = AlgorithmFactory.GetAlgorithmKeys().Select(key => new { Name = key }).ToArray();
            algorithmListBox.DataSource = dataSource;

            var obj = dataSource.First();
            algorithmListBox.ValueMember = nameof(obj.Name);


            var bindingAlgorithm = new Binding(nameof(algorithmListBox.SelectedValue), Model, nameof(Model.Algorithm));
            algorithmListBox.DataBindings.Add(bindingAlgorithm);

            var bindingDelaySliderToDelayTextBox = new Binding(nameof(delaySlider.Value), delayTextBox, nameof(delayTextBox.Text),
                true, DataSourceUpdateMode.OnPropertyChanged);
            delaySlider.DataBindings.Add(bindingDelaySliderToDelayTextBox);

            delaySlider.Minimum = Range.DelayValueRange.LowerRange;
            delaySlider.Maximum = Range.DelayValueRange.UpperRange;

            var bindingDelatTextBoxToModel = new Binding(nameof(delayTextBox.Text), Model, nameof(Model.DelayTime),
                true, DataSourceUpdateMode.OnPropertyChanged);
            delayTextBox.DataBindings.Add(bindingDelatTextBoxToModel);

        }
    }
}
