using System;

namespace Common.Extensions
{
    public static class MayBe
    {
        public static TResult With<TInput, TResult>(this TInput self, 
            Func<TInput, TResult> func)
            where TInput : class where TResult : class
        {
            if (self == null) return null;
            return func(self);
        }

        public static TResult Return<TInput, TResult>(this TInput self, 
            Func<TInput, TResult> func, TResult failureValue)
            where TInput : class where TResult : class
        {
            if (self == null) return failureValue;            
            return func(self);
        }

        public static TInput If<TInput>(this TInput self, 
            Predicate<TInput> evaluator)
            where TInput : class
        {
            if (self == null) return null;
            return evaluator(self) ? self : null;
        }

        public static TInput Do<TInput>(this TInput self, 
            Action<TInput> action)
            where TInput : class
        {
            if (self == null) return null;
            action(self);
            return self;
        }
    }
}
