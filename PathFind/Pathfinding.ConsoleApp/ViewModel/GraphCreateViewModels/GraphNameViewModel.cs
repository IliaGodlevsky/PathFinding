using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel.GraphCreateViewModels
{
    internal sealed class GraphNameViewModel
    {
        private readonly IMessenger messenger;

        public string GraphName { get; set; }

        public bool IsValid { get; set; }

        public GraphNameViewModel([KeyFilter(KeyFilters.ViewModels)]IMessenger messenger)
        {
            this.messenger = messenger;
        }
    }
}
