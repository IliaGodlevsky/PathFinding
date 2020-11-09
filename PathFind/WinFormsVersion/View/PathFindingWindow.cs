using Algorithm.AlgorithmCreating;
using Common.ValueRanges;
using System.Linq;
using System.Windows.Forms;
using WinFormsVersion.ViewModel;

namespace WinFormsVersion.View
{
    internal partial class PathFindingWindow : Form
    {
        public PathFindingWindow(PathFindingViewModel model)
        {
            InitializeComponent();

            okButton.Click += model.PathFind;
            cancelButton.Click += model.CancelPathFind;

            var dataSource = AlgorithmFactory.AlgorithmKeys.
                Select(key => new { Name = key }).ToArray();
            algorithmListBox.DataSource = dataSource;

            var algoKey = dataSource.First();
            algorithmListBox.ValueMember = nameof(algoKey.Name);

            var algorithmBinding = new Binding(
                nameof(algorithmListBox.SelectedValue), 
                model, 
                nameof(model.AlgorithmKey));
            algorithmListBox.DataBindings.Add(algorithmBinding);

            var bindingDelaySliderToDelayTextBox = new Binding(
                nameof(delaySlider.Value), 
                delayTextBox, 
                nameof(delayTextBox.Text),
                true, DataSourceUpdateMode.OnPropertyChanged);
            delaySlider.DataBindings.Add(bindingDelaySliderToDelayTextBox);

            delaySlider.Minimum = Range.DelayValueRange.LowerRange;
            delaySlider.Maximum = Range.DelayValueRange.UpperRange;

            var bindingDelatTextBoxToModel = new Binding(
                nameof(delayTextBox.Text), 
                model, 
                nameof(model.DelayTime),
                true, DataSourceUpdateMode.OnPropertyChanged);
            delayTextBox.DataBindings.Add(bindingDelatTextBoxToModel);
        }
    }
}
