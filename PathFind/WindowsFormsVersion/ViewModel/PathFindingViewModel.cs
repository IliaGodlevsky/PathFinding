using Algorithm.AlgorithmCreating;
using Common;
using Common.Interfaces;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel, IViewModel
    {
        public event EventHandler OnWindowClosed;

        public PathFindingViewModel(IMainModel model) : base(model)
        {
            int algorithmDelayTimeUpperRange
                    = Convert.ToInt32(ConfigurationManager.AppSettings["algorithmDelayTimeUpperRange"]);

            AlgorithmDelayTimeValueRange = new ValueRange(algorithmDelayTimeUpperRange, 0);
            AlgorithmKeys = AlgorithmFactory.AlgorithmsDescriptions.ToList();
        }

        public void PathFind(object sender, EventArgs e)
        {
            if (CanExecuteConfirmPathFindAlgorithmChoice())
            {
                OnWindowClosed?.Invoke(this, new EventArgs());
                FindPath();
            }
            OnWindowClosed = null;
        }

        protected override void OnAlgorithmIntermitted()
        {
            Application.DoEvents();
        }

        public void CancelPathFind(object sender, EventArgs e)
        {
            OnWindowClosed?.Invoke(this, new EventArgs());
            OnWindowClosed = null;
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice()
        {
            return AlgorithmKeys.Any(algo => algo == AlgorithmKey);
        }
    }
}
