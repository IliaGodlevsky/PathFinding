using AssembleClassesLib.Interface;
using GraphLib.Interfaces.Factories;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Realizations.Coordinates;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.ViewModel
{
    public abstract class GraphCreatingModel : IModel
    {
        public int Width { get; set; }

        public int Length { get; set; }

        public int ObstaclePercent { get; set; }

        public string GraphAssembleKey { get; set; }

        public IList<string> GraphAssembleKeys { get; set; }

        protected GraphCreatingModel(ILog log, IMainModel model, IAssembleClasses graphFactories)
        {
            GraphAssembleKeys = graphFactories.ClassesNames.ToList();
            this.model = model;
            graphAssembleClasses = graphFactories;
            this.log = log;
        }

        public virtual void CreateGraph()
        {
            try
            {
                var graphAssemble = (IGraphAssemble)graphAssembleClasses.Get(GraphAssembleKey);
                var graph = graphAssemble.AssembleGraph(ObstaclePercent, GraphParametres);
                model.ConnectNewGraph(graph);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected virtual int[] GraphParametres => new[] { Width, Length };

        protected readonly IMainModel model;
        protected readonly IAssembleClasses graphAssembleClasses;
        protected readonly ILog log;
    }
}
