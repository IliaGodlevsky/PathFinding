using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.Service.Interface.Models.Undefined
{
    public class CostCoordinatePair
    {
        public CoordinateModel Position { get; set; }

        public int Cost { get; set; }
    }
}
