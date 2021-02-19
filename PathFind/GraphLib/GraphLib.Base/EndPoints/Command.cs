using GraphLib.Interface;
using System;

namespace GraphLib.Base.EndPoints
{
    internal class Command
    {
        public Command(Predicate<IVertex> canExecute = null,
            Action<IVertex> execute = null)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public bool? CanExecute(IVertex vertex)
        {
            return canExecute?.Invoke(vertex);
        }

        public void Execute(IVertex vertex)
        {
            execute?.Invoke(vertex);
        }

        private readonly Predicate<IVertex> canExecute;
        private readonly Action<IVertex> execute;
    }
}
