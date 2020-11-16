using Common.ValueRanges;
using System.Windows.Forms;
using WinFormsVersion.ViewModel;

namespace WinFormsVersion.View
{
    internal partial class GraphCreatingWIndow : Form
    {
        public GraphCreatingViewModel Model { get; set; }

        public GraphCreatingWIndow(GraphCreatingViewModel model)
        {
            InitializeComponent();

            Model = model;

            okButton.Click += Model.CreateGraph;
            cancelButton.Click += Model.CancelCreateGraph;


            int ConvertFromString(string str, int alternativeResult)
            {
                return int.TryParse(str, out int number) 
                    ? int.Parse(str) 
                    : alternativeResult;
            }

            void StringToWidth(object sender, ConvertEventArgs e)
            {
                e.Value = ConvertFromString(
                    e.Value.ToString(), 
                    Range.WidthValueRange.LowerRange);
            }

            void StringToHeight(object sender, ConvertEventArgs e)
            {
                e.Value = ConvertFromString(
                    e.Value.ToString(), 
                    Range.HeightValueRange.LowerRange);
            }

            void IntToString(object sender, ConvertEventArgs e)
            {
                e.Value = e.Value.ToString();
            }

            var bindWidth = new Binding(
                nameof(widthTextBox.Text), 
                Model, 
                nameof(Model.Width));
            widthTextBox.DataBindings.Add(bindWidth);

            bindWidth.Format += IntToString;
            bindWidth.Parse += StringToWidth;

            var bindHeight = new Binding(
                nameof(heightTextBox.Text), 
                Model, 
                nameof(Model.Length));
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

            obstacleSlider.Maximum = Range.ObstaclePercentValueRange.UpperRange;
            obstacleSlider.Minimum = Range.ObstaclePercentValueRange.LowerRange;

            var bindObstaclePercent = new Binding(
                nameof(obstacleTextBox.Text), 
                Model, 
                nameof(Model.ObstaclePercent), 
                true,
                DataSourceUpdateMode.OnPropertyChanged);
            obstacleTextBox.DataBindings.Add(bindObstaclePercent);
        }
    }
}
