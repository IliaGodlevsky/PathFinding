using Conditional.Interfaces;
using System;

namespace Conditional
{
    internal sealed class ExceptionConditionConstruction<TException, T> 
        : IConditionConstruction<T>, IDisposable
        where TException : Exception
    {
        public ExceptionConditionConstruction(Predicate<T> condition, 
            params object[] exceptionParams)
        {
            this.condition = condition;
            exceptionParametres = exceptionParams;
        }

        public void ExecuteBody(T paramtre)
        {
            TException exception;
            try
            {
                exception = (TException)Activator.CreateInstance(typeof(TException), exceptionParametres);
            }
            catch(Exception ex)
            {
                throw new Exception($"{nameof(ExceptionConditionConstruction<TException, T>)} " +
                    $"tried to throw an exception of type {nameof(TException)}, but something was " +
                    $"wrong. A {nameof(Exception)} was thrown instead", ex);
            }
            throw exception;
        }

        public bool? IsCondition(T parametre)
        {
            return condition == null 
                || condition?.Invoke(parametre) == true;
        }

        public void Dispose()
        {
            condition = null;
            exceptionParametres = null;
        }

        private Predicate<T> condition;
        private object[] exceptionParametres;
    }
}
