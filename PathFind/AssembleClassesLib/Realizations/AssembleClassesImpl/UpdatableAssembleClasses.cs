using AssembleClassesLib.Interface;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using Interruptable.Interface;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AssembleClassesLib.Realizations
{
    public sealed class UpdatableAssembleClasses : IAssembleClasses, IInterruptable, IDisposable
    {
        public event InterruptEventHanlder OnInterrupted;
        public event Action<Exception, string> OnExceptionCaught;

        public UpdatableAssembleClasses(IAssembleClasses assembleClasses)
        {
            this.assembleClasses = assembleClasses;
            ClassesNames = assembleClasses.ClassesNames;
            tokenSource = new CancellationTokenSource();
            isStarted = false;
        }

        public bool IsInterruptRequested => token.IsCancellationRequested;

        public IReadOnlyCollection<string> ClassesNames { get; private set; }

        public object Get(string key, params object[] parametres)
        {
            return assembleClasses.Get(key, parametres);
        }

        public void LoadClasses()
        {
            if (!isStarted)
            {
                token = tokenSource.Token;
                isStarted = true;
                Task.Run(() =>
                {
                    while (!IsInterruptRequested)
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
        }

        public void Interrupt()
        {
            tokenSource.Cancel();
            isStarted = false;
            OnInterrupted?.Invoke(this, new InterruptEventArgs());
        }

        public void Dispose()
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
            OnInterrupted = null;
            isStarted = false;
        }

        private readonly IAssembleClasses assembleClasses;
        private readonly CancellationTokenSource tokenSource;
        private CancellationToken token;
        private bool isStarted;
    }
}