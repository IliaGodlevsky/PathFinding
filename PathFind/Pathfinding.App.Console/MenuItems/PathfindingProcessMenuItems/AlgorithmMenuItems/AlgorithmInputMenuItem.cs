﻿using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems.AlgorithmMenuItems
{
    internal abstract class AlgorithmInputMenuItem : AlgorithmMenuItem
    {
        protected readonly IInput<int> intInput;
        private readonly IReadOnlyDictionary<string, IStepRule> stepRules;
        private readonly IReadOnlyDictionary<string, IHeuristic> heuristics;

        protected AlgorithmInputMenuItem(IMessenger messenger,
            IReadOnlyDictionary<string, IStepRule> stepRules,
            IReadOnlyDictionary<string, IHeuristic> heuristics,
            IInput<int> intInput)
            : base(messenger)
        {
            this.stepRules = stepRules;
            this.heuristics = heuristics;
            this.intInput = intInput;
        }

        protected abstract IAlgorithmFactory<PathfindingProcess> CreateAlgorithm(IStepRule stepRule, IHeuristic heuristics);

        protected override AlgorithmInfo GetAlgorithm()
        {
            var stepRule = InputItem(stepRules, Languages.ChooseStepRuleMsg);
            var heuristic = InputItem(heuristics, Languages.ChooseHeuristicMsg);
            var factory = CreateAlgorithm(stepRule.Value, heuristic.Value);
            return new(factory, LanguageKey, stepRule.Key, heuristic.Key);
        }

        private static string GetString<T>(T key)
        {
            string name = key.ToString();
            return Languages.ResourceManager.GetString(name) ?? name;
        }

        private (TKey Key, TValue Value) InputItem<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> items, string inputMessage)
        {
            if (items == null || items.Count == 0)
            {
                return default;
            }

            if (items.Count == 1)
            {
                var item = items.First();
                return (item.Key, item.Value);
            }

            string menu = items.Keys.Select(GetString)
                .CreateMenuList(1)
                .ToString();

            using (Cursor.UseCurrentPositionWithClean())
            {
                int index = intInput.Input(menu + inputMessage, items.Count, 1) - 1;
                var item = items.ElementAt(index);
                return (item.Key, item.Value);
            }
        }
    }
}
