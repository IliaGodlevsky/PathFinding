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

            var bindWidth = new Binding("Text", Model, "Width");
            widthTextBox.DataBindings.Add(bindWidth);

            var bindHeight = new Binding("Text", Model, "Height");
            heightTextBox.DataBindings.Add(bindHeight);

            var bindTextBoxAndSlider = new Binding("Value", obstacleTextBox, "Text", true, 
                DataSourceUpdateMode.OnPropertyChanged);
            obstacleSlider.DataBindings.Add(bindTextBoxAndSlider);

            var bindObstaclePercent = new Binding("Text", Model, "ObstaclePercent", true,
                DataSourceUpdateMode.OnPropertyChanged);
            obstacleTextBox.DataBindings.Add(bindObstaclePercent);
        }
    }
}
