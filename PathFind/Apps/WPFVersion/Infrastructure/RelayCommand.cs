using System;

namespace WPFVersion.Infrastructure
{
    internal sealed class RelayCommand : BaseCommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public RelayCommand(Action<object> execute,
            Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
