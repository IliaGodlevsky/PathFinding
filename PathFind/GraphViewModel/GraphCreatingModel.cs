using Common.Extensions;
using GraphLib.Interfaces.Factories;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.Collections.Generic;

namespace GraphLib.ViewModel
{
    public abstract class GraphCreatingModel : IModel
    {
        public int Width { get; set; }

        public int Length { get; set; }

        public int ObstaclePercent { get; set; }

        public string GraphAssembleKey { get; set; }

        public IDictionary<string, IGraphAssemble> GraphAssembles { get; }

        public virtual IGraphAssemble SelectedGraphAssemble { get; set; }

        protected GraphCreatingModel(ILog log, IMainModel model,
            IEnumerable<IGraphAssemble> graphAssembles)
        {
            GraphAssembles = graphAssembles.AsNameInstanceDictionary();
            this.model = model;
            this.graphAssembles = graphAssembles;
            this.log = log;
        }

        public virtual void CreateGraph()
        {
            try
            {
                var graph = SelectedGraphAssemble.AssembleGraph(ObstaclePercent, GraphParametres);
                model.ConnectNewGraph(graph);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected virtual int[] GraphParametres => new[] { Width, Length };

        protected readonly IMainModel model;
        protected readonly IEnumerable<IGraphAssemble> graphAssembles;
        protected readonly ILog log;
    }
}
