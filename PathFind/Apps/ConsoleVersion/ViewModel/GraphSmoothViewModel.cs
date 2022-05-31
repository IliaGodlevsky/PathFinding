﻿using Autofac;
using Common.Extensions;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using EnumerationValues.Realizations;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Enums;
using GraphLib.Realizations.Extensions;
using GraphLib.Realizations.SmoothLevel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleVersion.ViewModel
{
    internal class GraphSmoothViewModel : IViewModel, IRequireIntInput, IDisposable
    {
        public event Action WindowClosed;

        private readonly IMeanCost meanAlgorithm;
        private readonly IVertexCostFactory costFactory;
        private readonly IMessenger messenger;

        private IGraph graph;

        public IInput<int> IntInput { get; set; }

        public IReadOnlyList<ISmoothLevel> SmoothLevels { get; }

        public string ChooseSmoothLevelMsg { private get; set; }

        private int SmoothLevelIndex => IntInput.Input(ChooseSmoothLevelMsg, SmoothLevels.Count, 1) - 1;

        private ISmoothLevel SmoothLevel => SmoothLevels[SmoothLevelIndex];

        public GraphSmoothViewModel(IMeanCost meanAlgorithm, IVertexCostFactory costFactory)
        {
            SmoothLevels = EnumValuesWithoutIgnored<SmoothLevels>
                .Create()
                .Values
                .Select(item => (ISmoothLevel)item.GetAttributeOrNull<SmoothLevelAttribute>())
                .ToArray();
            messenger = DI.Container.Resolve<IMessenger>();
            this.costFactory = costFactory;
            this.meanAlgorithm = meanAlgorithm;
            messenger.Register<ClaimGraphAnswer>(this, GetGraph);
            messenger.Send(new ClaimGraphMessage());
        }

        [MenuItem(MenuItemsNames.SmoothGraph, 0)]
        private void SmoothGraph()
        {
            graph.Smooth(costFactory, meanAlgorithm, SmoothLevel.Level);
        }

        [MenuItem(MenuItemsNames.Exit, 1)]
        private void Interrup()
        {
            WindowClosed?.Invoke();
        }

        public void Dispose()
        {
            messenger.Unregister(this);
            WindowClosed = null;
        }

        private void GetGraph(ClaimGraphAnswer answer)
        {
            graph = answer.Graph;
        }
    }
}
