﻿using System;

namespace Pathfinding.App.Console.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal sealed class BsonTableAttribute : Attribute
    {
        public string Name { get; }

        public BsonTableAttribute(string name)
        {
            Name = name;
        }
    }
}