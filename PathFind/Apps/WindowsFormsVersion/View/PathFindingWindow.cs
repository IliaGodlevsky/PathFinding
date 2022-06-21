using System;
using System.Windows.Forms;
using ValueRange.Extensions;
using WindowsFormsVersion.Attributes;
using WindowsFormsVersion.ViewModel;

namespace WindowsFormsVersion.View
{
    [AppWindow]
    internal partial class PathFindingWindow : ViewModelWindow
    {
        public PathFindingWindow(PathFindingViewModel model) : base(model)
        {
            InitializeComponent();

            okButton.Click += model.StartPathfinding;
            cancelButton.Click += model.CancelPathFinding;

            var dataSource = model.Algorithms;
            algorithmListBox.DataSource = dataSource;

            var algorithmBinding = new Binding(
                nameof(algorithmListBox.SelectedItem),
                model,
                nameof(model.Algorithm),
                true,
                DataSourceUpdateMode.OnPropertyChanged);
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

            delaySlider.Minimum = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange.Milliseconds;
            delaySlider.Maximum = Constants.AlgorithmDelayTimeValueRange.UpperValueOfRange.Milliseconds;

            var bindingDelatTextBoxToModel = new Binding(
                nameof(delayTextBox.Text),
                model,
                nameof(model.Delay),
                true, DataSourceUpdateMode.OnPropertyChanged);

            delayTextBox.DataBindings.Add(bindingDelatTextBoxToModel);
            bindingDelatTextBoxToModel.Format += TimeToString;
            bindingDelatTextBoxToModel.Parse += StringToDelay;
        }

        private void TimeToString(object sender, ConvertEventArgs e)
        {
            if (e.Value is TimeSpan time)
            {
                e.Value = time.Milliseconds.ToString();
            }
        }

        private void StringToDelay(object sender, ConvertEventArgs e)
        {
            int alternativeResult = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange.Milliseconds;
            int value = int.TryParse(e.Value.ToString(), out int number) ? number : alternativeResult;
            var time = TimeSpan.FromMilliseconds(value);
            time = Constants.AlgorithmDelayTimeValueRange.ReturnInRange(time);
            e.Value = time;
        }
    }
}