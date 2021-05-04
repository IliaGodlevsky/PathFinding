using AssembleClassesLib.EventArguments;
using AssembleClassesLib.Interface;
using AssembleClassesLib.Realizations.AssembleClassesImpl;
using Common.Interface;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AssembleClassesLib.Realizations
{
    public sealed class UpdatableAssembleClasses
        : IAssembleClasses, IInterruptable, IDisposable
    {
        public event EventHandler OnInterrupted;
        public event Action<Exception, string> OnExceptionCaught;

        public UpdatableAssembleClasses(AssembleClasses assembleClasses)
           : this((IAssembleClasses)assembleClasses)
        {

        }

        public UpdatableAssembleClasses(INotifingAssembleClasses assembleClasses)
            : this((IAssembleClasses)assembleClasses)
        {

        }

        public IReadOnlyCollection<string> ClassesNames { get; private set; }

        public object Get(string key, params object[] parametres)
        {
            return assembleClasses.Get(key, parametres);
        }

        public void LoadClasses()
        {
            Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        assembleClasses.LoadClasses();
                        ClassesNames = assembleClasses.ClassesNames;
                    }
                    catch (Exception ex)
                    {
                        OnExceptionCaught?.Invoke(ex, string.Empty);
                    }
                }
            }, token);
        }

        public void Interrupt()
        {
            tokenSource.Cancel();
            var args = new AssembleClassesEventArgs(ClassesNames);
            OnInterrupted?.Invoke(this, args);
        }

        public void Dispose()
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
            OnInterrupted = null;
        }

        private UpdatableAssembleClasses(IAssembleClasses assembleClasses)
        {
            this.assembleClasses = assembleClasses;
            ClassesNames = assembleClasses.ClassesNames;
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
        }

        private readonly IAssembleClasses assembleClasses;
        private readonly CancellationToken token;
        private readonly CancellationTokenSource tokenSource;
    }
}