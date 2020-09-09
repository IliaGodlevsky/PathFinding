using GraphLibrary.ValueRanges;
using System.Windows.Forms;
using WinFormsVersion.ViewModel;

namespace WinFormsVersion.View
{
    internal partial class CreateGraphWindow : Form
    {
        public CreateGraphViewModel Model { get; set; }
        public CreateGraphWindow(CreateGraphViewModel model)
        {
            InitializeComponent();

            Model = model;

            okButton.Click += Model.CreateGraph;
            cancelButton.Click += Model.CancelCreateGraph;


            int ConvertFromString(string str, int alternativeResult)
            {
                if (int.TryParse(str, out int number))
                    return int.Parse(str);
                else
                    return alternativeResult;
            }

            void StringToWidth(object sender, ConvertEventArgs e)
            {
                e.Value = ConvertFromString(e.Value.ToString(), Range.WidthValueRange.LowerRange);
            }

            void StringToHeight(object sender, ConvertEventArgs e)
            {
                e.Value = ConvertFromString(e.Value.ToString(), Range.HeightValueRange.LowerRange);
            }

            void IntToString(object sender, ConvertEventArgs e)
            {
                e.Value = e.Value.ToString();
            }



            var bindWidth = new Binding("Text", Model, "Width");
            widthTextBox.DataBindings.Add(bindWidth);
            bindWidth.Format += IntToString;
            bindWidth.Parse += StringToWidth;

            var bindHeight = new Binding("Text", Model, "Height");
            heightTextBox.DataBindings.Add(bindHeight);
            bindHeight.Format += IntToString;
            bindHeight.Parse += StringToHeight;

            var bindTextBoxAndSlider = new Binding("Value", obstacleTextBox, "Text", true, 
                DataSourceUpdateMode.OnPropertyChanged);
            obstacleSlider.DataBindings.Add(bindTextBoxAndSlider);

            obstacleSlider.Maximum = Range.ObstaclePercentValueRange.UpperRange;
            obstacleSlider.Minimum = Range.ObstaclePercentValueRange.LowerRange;

            var bindObstaclePercent = new Binding("Text", Model, "ObstaclePercent", true,
                DataSourceUpdateMode.OnPropertyChanged);
            obstacleTextBox.DataBindings.Add(bindObstaclePercent);
        }
    }
}
