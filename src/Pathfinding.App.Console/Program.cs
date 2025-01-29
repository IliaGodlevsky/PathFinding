using Autofac;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.View;
using Terminal.Gui;

Application.Init();
using var scope = Modules.Build();
var main = scope.Resolve<MainView>();
Application.Top.Add(main);
Application.Run(x => true);