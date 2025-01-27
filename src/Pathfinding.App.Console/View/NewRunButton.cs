using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.View;
using Pathfinding.App.Console.ViewModel;
using Terminal.Gui;

namespace Pathfinding.App.Console.View
{
    internal sealed partial class NewRunButton : Button
    {
        private readonly IMessenger messenger;
        private readonly NewRunButtonViewModel viewModel;

        public NewRunButton(
            [KeyFilter(KeyFilters.Views)] IMessenger messenger,
            NewRunButtonViewModel viewModel)
        {
            Initialize();
            this.messenger = messenger;
            this.viewModel = viewModel;
            MouseClick += OnClick;
        }

        private void OnClick(MouseEventArgs e)
        {
            if (e.MouseEvent.Flags == MouseFlags.Button1Clicked && viewModel.CanCreate())
            {
                messenger.Send(new OpenAlgorithmCreationViewMessage());
            }
        }
    }
}
