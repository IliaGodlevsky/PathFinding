using System.Linq;
using System.Windows.Forms;
using WindowsFormsVersion.ViewModel;

namespace WindowsFormsVersion.View
{
    internal partial class PathFindingWindow : Form
    {
        public PathFindingWindow(PathFindingViewModel model)
        {
            InitializeComponent();

            okButton.Click += model.StartPathfinding;
            cancelButton.Click += model.CancelPathFinding;

            var dataSource = model.Algorithms.ToArray();
            algorithmListBox.DataSource = dataSource;

            algorithmListBox.ValueMember = "Item2";
            algorithmListBox.DisplayMember = "Item1";

            var algorithmBinding = new Binding(
                nameof(algorithmListBox.SelectedValue),
                model,
                nameof(model.Algorithm));
            algorithmListBox.DataBindings.Add(algorithmBinding);

            var bindingVisualize = new Binding(
                nameof(visualizeCheckBox.Checked),
                model,
                nameof(model.IsVisualizationRequired));
            visualizeCheckBox.DataBindings.Add(bindingVisualize);

            var bindingDelaySliderToDelayTextBox = new Binding(
                nameof(delaySlider.Value),
                delayTextBox,
                nameof(delayTextBox.Text),
                true, DataSourceUpdateMode.OnPropertyChanged);
            delaySlider.DataBindings.Add(bindingDelaySliderToDelayTextBox);

            delaySlider.Minimum = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange;
            delaySlider.Maximum = Constants.AlgorithmDelayTimeValueRange.UpperValueOfRange;

            var bindingDelatTextBoxToModel = new Binding(
                nameof(delayTextBox.Text),
                model,
                nameof(model.DelayTime),
                true, DataSourceUpdateMode.OnPropertyChanged);
            delayTextBox.DataBindings.Add(bindingDelatTextBoxToModel);
        }
    }
}
