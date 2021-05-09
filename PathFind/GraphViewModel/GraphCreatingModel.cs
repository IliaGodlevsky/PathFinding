using Common.Logging;
using GraphLib.Interfaces.Factories;
using GraphViewModel.Interfaces;
using System;

namespace GraphLib.ViewModel
{
    public abstract class GraphCreatingModel : IModel
    {
        public event Action<string> OnEventHappened;

        public int Width { get; set; }

        public int Length { get; set; }

        public int ObstaclePercent { get; set; }

        protected GraphCreatingModel(IMainModel model, IGraphAssemble graphFactory)
        {
            this.model = model;
            this.graphFactory = graphFactory;
        }

        protected void RaiseOnEventHappened(string message)
        {
            OnEventHappened?.Invoke(message);
        }

        public virtual void CreateGraph()
        {
            try
            {
                var graph = graphFactory.AssembleGraph(ObstaclePercent, GraphParametres);
                model.ConnectNewGraph(graph);
            }
            catch (Exception ex)
            {
                RaiseOnEventHappened(ex.Message);
                Logger.Instance.Error(ex);
            }
            finally
            {
                OnEventHappened = null;
            }
        }

        protected virtual int[] GraphParametres => new[] { Width, Length };

        protected IMainModel model;
        protected IGraphAssemble graphFactory;
    }
}
