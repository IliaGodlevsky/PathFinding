using ConsoleVersion.View.Abstraction;
using ConsoleVersion.View.Interface;
using ConsoleVersion.ViewModel;
using GraphLib.Base;
using GraphLib.Interfaces.Factories;
using GraphViewModel.Interfaces;
using Interruptable.Interface;
using Logging.Interface;
using System;
using System.Collections.Generic;

namespace ConsoleVersion.View
{
    internal static class ViewFactory
    {
        public static IView CreateView<TView, TModel>(MainViewModel mainModel, ILog log, object parameter)
            where TView : View<TModel>
            where TModel : IModel, IInterruptable
        {
            var model = (TModel)Activator.CreateInstance(typeof(TModel), log, mainModel, parameter);
            var view = (TView)Activator.CreateInstance(typeof(TView), model);
            view.OnNewMenuIteration += mainModel.DisplayGraph;
            model.OnInterrupted += view.OnInterrupted;
            return view;
        }

        public static void StartView<TView, TModel>(MainViewModel mainModel, ILog log, object parameter)
            where TView : View<TModel>
            where TModel : IModel, IInterruptable
        {
            var view = (TView)CreateView<TView, TModel>(mainModel, log, parameter);
            view.Start();
            view.OnNewMenuIteration -= mainModel.DisplayGraph;
        }

        public static void StartPathFindingView(MainViewModel mainModel, ILog log, BaseEndPoints endPoints)
        {
            StartView<PathFindView, PathFindingViewModel>(mainModel, log, endPoints);
        }

        public static void StartGraphCreatingView(MainViewModel mainModel, ILog log, IEnumerable<IGraphAssemble> graphAssembles)
        {
            StartView<GraphCreateView, GraphCreatingViewModel>(mainModel, log, graphAssembles);
        }
    }
}
