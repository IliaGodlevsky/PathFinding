using Common;
using GraphLib.Interface;
using GraphViewModel.Interfaces;
using System;

namespace GraphLib.ViewModel
{
    public abstract class GraphCreatingModel : IModel
    {
        public ValueRange GraphWidthValueRange { get; protected set; }

        public ValueRange GraphLengthValueRange { get; protected set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public int ObstaclePercent { get; set; }

        public ValueRange ObstaclePercentValueRange { get; }

        public GraphCreatingModel(IMainModel model,
            IGraphAssembler graphFactory)
        {
            this.model = model;
            this.graphFactory = graphFactory;
            ObstaclePercentValueRange = new ValueRange(99, 0);
        }

        public virtual void CreateGraph()
        {
            try
            {
                var graph = graphFactory.AssembleGraph(ObstaclePercent, GraphParametres);
                model.ConnectNewGraph(graph);
            }
            catch(Exception ex)
            {
                Logger.Instance.Log(ex);
            }
        }

        protected virtual int[] GraphParametres => new int[] { Width, Length };

        protected IMainModel model;
        protected IGraphAssembler graphFactory;
    }
}
