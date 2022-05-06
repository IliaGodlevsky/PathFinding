using Autofac;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Messages;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Interfaces;
using GraphLib.Serialization.Extensions;
using GraphLib.Serialization.Interfaces;
using NullObject.Extensions;
using System;

namespace ConsoleVersion.ViewModel
{
    internal sealed class GraphSaveViewModel : IViewModel, IDisposable
    {
        public event Action WindowClosed;

        private readonly IMessenger messenger;
        private readonly IGraphSerializationModule module;

        private IGraph Graph { get; set; }

        public GraphSaveViewModel(IGraphSerializationModule module)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<ClaimGraphAnswer>(this, OnClaimAnswerRecieved);
            messenger.Send(new ClaimGraphMessage());
            this.module = module;
        }

        public void Dispose()
        {
            messenger.Unregister(this);
            WindowClosed = null;
        }

        [PreValidationMethod(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.SaveGraph, 0)]
        private async void SaveGraph()
        {
            await module.SaveGraphAsync(Graph);
        }

        [MenuItem(MenuItemsNames.Exit, 1)]
        private void Interrupt()
        {
            WindowClosed?.Invoke();
        }

        private void OnClaimAnswerRecieved(ClaimGraphAnswer answer)
        {
            Graph = answer.Graph;
        }

        private bool IsGraphValid()
        {
            return !Graph.IsNull();
        }
    }
}
