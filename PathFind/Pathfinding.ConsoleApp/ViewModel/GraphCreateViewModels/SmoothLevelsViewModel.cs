using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages;
using Pathfinding.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.ConsoleApp.ViewModel.GraphCreateViewModels
{
    internal sealed class SmoothLevelsViewModel
    {
        private readonly IMessenger messenger;

        public int SmoothLevel { get; set; }

        public IReadOnlyDictionary<string, int> SmoothLevels { get; }

        public SmoothLevelsViewModel(IEnumerable<(string Name, int Level)> levels, 
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger)
        {
            SmoothLevels = levels.ToDictionary(x => x.Name, x => x.Level).AsReadOnly();
            this.messenger = messenger;
            messenger.Register<GraphParametresRequest>(this, OnGraphParametresRequestRecieved);
        }

        private void OnGraphParametresRequestRecieved(object recipient, GraphParametresRequest request)
        {
            request.SmoothLevel = SmoothLevel;
        }
    }
}
