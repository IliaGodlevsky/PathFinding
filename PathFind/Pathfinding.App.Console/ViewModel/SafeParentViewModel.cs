using Pathfinding.App.Console.Interface;
using Pathfinding.Logging.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.ViewModel
{
    internal abstract class SafeParentViewModel : SafeViewModel, IParentViewModel
    {
        public IReadOnlyCollection<IViewModel> Children { get; set; }

        public IViewFactory ViewFactory { get; set; }

        protected SafeParentViewModel(ILog log) : base(log)
        {
        }

        protected void Display<TViewModel>() where TViewModel : IViewModel
        {
            var model = Children.SingleOrDefault(model => model is TViewModel);
            if (model != null)
            {
                var view = ViewFactory.CreateView(model);
                view.Display();
            }
        }

        protected bool IsOperationSuppoted<TViewModel>() where TViewModel : IViewModel
        {
            return Children.SingleOrDefault(model => model is TViewModel) != null;
        }
    }
}