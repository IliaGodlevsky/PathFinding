﻿using System;

namespace Pathfinding.App.Console.Menu.Realizations.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class FailMessageAttribute : Attribute
    {
        public static readonly FailMessageAttribute Default
            = new FailMessageAttribute(string.Empty);

        public string Message { get; }

        public FailMessageAttribute(string message)
        {
            Message = message;
        }
    }
}