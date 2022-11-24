using Pathfinding.App.WPF._3D.Infrastructure.Commands;
using Pathfinding.App.WPF._3D.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Pathfinding.App.WPF._3D.ViewModel
{
    public class OpacityChangeViewModel : IViewModel, IDisposable
    {
        public event Action WindowClosed;

        public ICommand ConfirmOpacityChange { get; }

        public ICommand CancelOpacityChange { get; }

        internal IEnumerable<IChangeColorOpacity> OpacityChangers { get; set; }

        public OpacityChangeViewModel()
        {
            OpacityChangers = Enumerable.Empty<IChangeColorOpacity>();
            ConfirmOpacityChange = new RelayCommand(ExecuteChangeVertexOpacity);
            CancelOpacityChange = new RelayCommand(ExecuteCloseChangeVertexOpacity);
        }

        public void Dispose()
        {
            WindowClosed = null;
        }

        private void ExecuteChangeVertexOpacity(object param)
        {
            OpacityChangers.ForEach(model => model.ChangeOpacity());
            ExecuteCloseChangeVertexOpacity(param);
        }

        private void ExecuteCloseChangeVertexOpacity(object param)
        {
            WindowClosed?.Invoke();
        }
    }
}