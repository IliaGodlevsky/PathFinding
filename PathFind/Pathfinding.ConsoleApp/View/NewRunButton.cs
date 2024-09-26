using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.View;
using Pathfinding.ConsoleApp.ViewModel;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class NewRunButton : Button
    {
        private readonly IMessenger messenger;
        private readonly NewRunButtonViewModel viewModel;
        private readonly CompositeDisposable disposables = new();

        public NewRunButton([KeyFilter(KeyFilters.Views)] IMessenger messenger,
            NewRunButtonViewModel viewModel)
        {
            Initialize();
            this.messenger = messenger;
            this.viewModel = viewModel;
            //viewModel.WhenAnyValue(x => x.GraphId)
            //    .Select(x => x == 0 ? Create(Color.Red) : Create(Color.Gray))
            //    .BindTo(this, x => x.ColorScheme)
            //    .DisposeWith(disposables);
            MouseClick += OnClick;
        }

        private void OnClick(MouseEventArgs e)
        {
            if (e.MouseEvent.Flags == MouseFlags.Button1Clicked && viewModel.CanCreate())
            {
                messenger.Send(new OpenRunCreationViewMessage());
            }
        }

        private ColorScheme Create(Color foreground)
        {
            return new()
            {
                Normal = Application.Driver.MakeAttribute(foreground, ColorContants.BackgroundColor),
                HotNormal = ColorScheme.HotNormal,
                Disabled = ColorScheme.Disabled,
                Focus = ColorScheme.Focus,
                HotFocus = ColorScheme.HotFocus,
            };
        }
    }
}
