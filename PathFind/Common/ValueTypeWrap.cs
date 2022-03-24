﻿namespace Common
{
    public sealed class ValueTypeWrap<T> where T : struct
    {
        public T Value { get; set; }
    }
}