using Autofac;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.ViewModel;

var model = DI.Container.Resolve<MainViewModel>();
var viewFactory = DI.Container.Resolve<IViewFactory>();
var view = viewFactory.CreateView(model);
view.Display();