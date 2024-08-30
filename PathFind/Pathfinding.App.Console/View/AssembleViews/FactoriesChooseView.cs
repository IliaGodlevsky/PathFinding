using Pathfinding.App.Console.ViewModel;
using System.Linq;
using Terminal.Gui;
using ReactiveMarbles.ObservableEvents;
using System.Reactive.Linq;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Collections.Generic;
using Pathfinding.Domain.Interface.Factories;

namespace Pathfinding.App.Console.View.AssembleViews
{
    public class FactoriesChooseView : Terminal.Gui.View
    {
        private readonly CompositeDisposable disposables;

        public FactoriesChooseView(GraphAssembleViewModel viewModel,
            IReadOnlyCollection<(string Name, INeighborhoodFactory)> neighborhoodFactories,
            IReadOnlyCollection<(string Name, IGraphAssemble<VertexViewModel>)> assemblers)
        {
            disposables = new();
            Data = viewModel;

            var factoryLabel = new Label("Neighborhood factories") { X = 1, Y = 1 };
            var factories = new ComboBox()
            {
                X = Pos.Left(factoryLabel),
                Y = Pos.Bottom(factoryLabel),
                Width = 20,
                Height = 1,
                ReadOnly = true
            };
            var factoriesDictionaries = viewModel.Factories
                .ToDictionary(x => x.Name, x => x.Factory);
            factories.SetSource(factoriesDictionaries.Keys.ToList());
            factories
                .Events()
                .SelectedItemChanged
                .Select(x => factoriesDictionaries[(string)x.Value])
                .DistinctUntilChanged()
                .BindTo(viewModel, x => x.Factory)
                .DisposeWith(disposables);

            var assemblerLabel = new Label("Graph assemblers")
            {
                X = Pos.Right(factories) + 4,
                Y = Pos.Top(factoryLabel)
            };
            var assemblersDictionaries = viewModel.Assemblers
                .ToDictionary(x => x.Name, x => x.Assembler);
            var assemblerFactories = new ComboBox()
            {
                X = Pos.Left(assemblerLabel),
                Y = Pos.Bottom(assemblerLabel),
                Width = 20,
                Height = 1,
                ReadOnly = true
            };
            assemblerFactories.SetSource(assemblersDictionaries.Keys.ToList());
            assemblerFactories
                .Events()
                .SelectedItemChanged
                .Select(x => assemblersDictionaries[(string)x.Value])
                .DistinctUntilChanged()
                .BindTo(viewModel, x => x.Assembler)
                .DisposeWith(disposables);

            Application.Top.Add(factoryLabel,
                factories,
                assemblerLabel,
                assemblerFactories);
        }
    }
}
