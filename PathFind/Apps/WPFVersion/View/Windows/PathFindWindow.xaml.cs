using System;
using System.Windows;
using WPFVersion.Attributes;
using WPFVersion.ViewModel;

namespace WPFVersion.View.Windows
{
    public partial class PathFindWindow : ViewModelWindow
    {
        public PathFindWindow(PathFindingViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();          
            delayTimeSlider.Minimum = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange;
            delayTimeSlider.Maximum = Constants.AlgorithmDelayTimeValueRange.UpperValueOfRange;
        }
    }
}
