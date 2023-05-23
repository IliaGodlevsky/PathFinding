using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model.Visualizations.Visuals
{
    internal abstract class Visual : IVisual, ICanRecieveMessage
    {
        public event Action<Vertex> Visualized;

        private readonly HashSet<Vertex> vertices = new();
        private readonly IMessenger messenger;

        protected abstract ConsoleColor Color { get; set; }

        protected abstract IToken Token { get; }

        protected Visual(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public virtual bool Contains(Vertex vertex)
        {
            return vertices.Contains(vertex);
        }

        public virtual void Visualize(Vertex vertex)
        {
            vertex.Color = Color;
            vertices.Add(vertex);
            Visualized?.Invoke(vertex);
        }

        public void Remove(Vertex vertex)
        {
            vertices.Remove(vertex);
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterData<ConsoleColor>(this, Token, SetColor);
        }

        private void SetColor(ConsoleColor color)
        {
            Color = color;
            vertices.ForEach(vertex => vertex.Color = Color);
        }
    }
}