using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Collections;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.History
{
    public sealed class History<TVolume, TColor>
        where TVolume : IHistoryVolume<TColor>, new()
    {
        private readonly int volumesCount = 5;
        private readonly List<TVolume> volumes = new List<TVolume>();

        public History()
        {
            
        }

        public void Remove(Guid key)
        {
            volumes.ForEach(volume => volume.Remove(key));
        }

        public void Clear()
        {
            volumes.ForEach(volume => volume.RemoveAll());
        }
    }
}
