using Common.Extensions.EnumerableExtensions;
using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Interface;

namespace WPFVersion3D.ViewModel
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