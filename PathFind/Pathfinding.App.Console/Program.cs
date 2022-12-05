using Autofac;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.ViewModel;
using Pathfinding.App.Console.Views;

var model = DI.Container.Resolve<MainViewModel>();
var view = DI.Container.Resolve<View>();
view.Display(model);