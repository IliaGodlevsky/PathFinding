﻿using Autofac.Features.Metadata;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class StepRulesViewModel
    {
        public IReadOnlyDictionary<string, IStepRule> StepRules { get; }

        public StepRulesViewModel(IEnumerable<(string Name, IStepRule StepRule)> stepRules)
        {
            StepRules = stepRules.ToDictionary(x => x.Name, x => x.StepRule).AsReadOnly();
        }

        public StepRulesViewModel(IEnumerable<Meta<IStepRule>> stepRules)
        {
            StepRules = stepRules.ToDictionary(x => (string)x.Metadata[MetadataKeys.NameKey], x => x.Value).AsReadOnly();
        }
    }
}
