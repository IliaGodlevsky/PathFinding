using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.GraphLib.Factory.Interface
{
    public interface ILayer<TGraph, TVertex>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex
    {
        void Overlay(TGraph graph);
    }
}
