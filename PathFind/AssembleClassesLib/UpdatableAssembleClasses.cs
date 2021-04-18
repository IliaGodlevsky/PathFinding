using System;
using System.Threading;
using System.Threading.Tasks;
using AssembleClassesLib.EventArguments;
using AssembleClassesLib.EventHandlers;
using AssembleClassesLib.Interface;
using Common.Interface;

namespace AssembleClassesLib
{
    public sealed class UpdatableAssembleClasses : IAssembleClasses, IInterruptable
    {
        public event EventHandler OnInterrupted;
        public event AssembleClassesEventHandler OnClassesLoaded;

        public UpdatableAssembleClasses(IAssembleClasses assembleClasses)
        {
            this.assembleClasses = assembleClasses;
            ClassesNames = assembleClasses.ClassesNames;
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
        }

        public string[] ClassesNames { get; private set; }

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
                    assembleClasses.LoadClasses();
                    ClassesNames = assembleClasses.ClassesNames;
                    var args = new AssembleClassesEventArgs(ClassesNames);
                    OnClassesLoaded?.Invoke(this, args);
                }
            }, token);
        }

        public void Interrupt()
        {
            tokenSource.Cancel();
            var args = new AssembleClassesEventArgs(ClassesNames);
            OnInterrupted?.Invoke(this, args);
            Dispose();
        }

        public void Dispose()
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
            OnInterrupted = null;
            OnClassesLoaded = null;
        }

        private readonly IAssembleClasses assembleClasses;
        private readonly CancellationToken token;
        private readonly CancellationTokenSource tokenSource;
    }
}