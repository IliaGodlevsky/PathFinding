using System.Linq;
using System.Windows.Forms;
using ValueRange.Extensions;
using WindowsFormsVersion.Attributes;
using WindowsFormsVersion.ViewModel;

namespace WindowsFormsVersion.View
{
    [AppWindow]
    internal partial class GraphCreatingWindow : ViewModelWindow
    {
        public GraphCreatingWindow(GraphCreatingViewModel model) : base(model)
        {
            InitializeComponent();

            okButton.Click += model.CreateGraph;
            cancelButton.Click += model.CancelCreateGraph;

            var source = model.GraphAssembles.ToArray();
            graphAssemblesListBox.DataSource = source;

            var graphAssembleBinding = new Binding(
                nameof(graphAssemblesListBox.SelectedItem),
                model,
                nameof(model.SelectedGraphAssemble),
                true,
                DataSourceUpdateMode.OnPropertyChanged);            
            graphAssemblesListBox.DataBindings.Add(graphAssembleBinding);
            graphAssemblesListBox.SelectedIndex = 0;

            var bindWidth = new Binding(
                nameof(widthTextBox.Text),
                model,
                nameof(model.Width));
            widthTextBox.DataBindings.Add(bindWidth);

            bindWidth.Format += IntToString;
            bindWidth.Parse += StringToWidth;

            var bindHeight = new Binding(
                nameof(heightTextBox.Text),
                model,
                nameof(model.Length));
            heightTextBox.DataBindings.Add(bindHeight);

            bindHeight.Format += IntToString;
            bindHeight.Parse += StringToHeight;

            var bindTextBoxAndSlider = new Binding(
                nameof(obstacleSlider.Value),
                obstacleTextBox,
                nameof(obstacleTextBox.Text),
                true,
                DataSourceUpdateMode.OnPropertyChanged);
            obstacleSlider.DataBindings.Add(bindTextBoxAndSlider);

            obstacleSlider.Maximum = Constants.ObstaclesPercentValueRange.UpperValueOfRange;
            obstacleSlider.Minimum = Constants.ObstaclesPercentValueRange.LowerValueOfRange;

            var bindObstaclePercent = new Binding(
                nameof(obstacleTextBox.Text),
                model,
                nameof(model.ObstaclePercent),
                true,
                DataSourceUpdateMode.OnPropertyChanged);
            obstacleTextBox.DataBindings.Add(bindObstaclePercent);
        }

        private int ConvertFromString(string str, int alternativeResult)
        {
            return int.TryParse(str, out int number)
                ? number
                : alternativeResult;
        }

        private void StringToWidth(object sender, ConvertEventArgs e)
        {
            var value = ConvertFromString(
                e.Value.ToString(),
                Constants.GraphWidthValueRange.LowerValueOfRange);

            value = Constants.GraphWidthValueRange.ReturnInRange(value);

            e.Value = value;
        }

        private void StringToHeight(object sender, ConvertEventArgs e)
        {
            var value = ConvertFromString(
                e.Value.ToString(),
                Constants.GraphLengthValueRange.LowerValueOfRange);

            value = Constants.GraphLengthValueRange.ReturnInRange(value);

            e.Value = value;
        }

        private void IntToString(object sender, ConvertEventArgs e)
        {
            e.Value = e.Value.ToString();
        }
    }
}