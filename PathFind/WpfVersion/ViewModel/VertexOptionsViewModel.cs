using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfVersion.Infrastructure;

namespace WpfVersion.ViewModel
{
    public class VertexOptionsViewModel
    {
        private MainWindowViewModel model;

        public RelayCommand ConfirmColorChoiceCommand { get; }
        public RelayCommand CancelColorChoiceCommand { get; }
        public VertexOptionsViewModel(MainWindowViewModel model)
        {
            this.model = model;
        }


    }
}
