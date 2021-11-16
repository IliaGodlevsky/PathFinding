using Common.Interface;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GraphLib.Realizations.Neighbourhoods
{
    [Serializable]
    public sealed class VonNeumannNeighborhood : INeighborhood, ISerializable, ICloneable<INeighborhood>
    {
        public IReadOnlyCollection<ICoordinate> Neighbours => neighbourhood.Value;

        public VonNeumannNeighborhood(ICoordinate coordinate)
        {
            neighbourhood = new Lazy<IReadOnlyCollection<ICoordinate>>(DetectNeighborhood);
            selfCoordinate = coordinate;
            neighboursCoordinates = new MooreNeighborhood(coordinate);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(selfCoordinate), selfCoordinate, typeof(ICoordinate));
        }

        private VonNeumannNeighborhood(SerializationInfo info, StreamingContext context)
            : this((ICoordinate)info.GetValue(nameof(selfCoordinate), typeof(ICoordinate)))
        {

        }

        private bool IsCardinal(ICoordinate coordinate)
        {
            return coordinate.IsCardinal(selfCoordinate);
        }

        private IReadOnlyCollection<ICoordinate> DetectNeighborhood()
        {
            return neighboursCoordinates.Neighbours.Where(IsCardinal).ToArray();
        }

        public INeighborhood Clone()
        {
            return new VonNeumannNeighborhood(selfCoordinate.Clone());
        }

        private readonly ICoordinate selfCoordinate;
        private readonly INeighborhood neighboursCoordinates;
        private readonly Lazy<IReadOnlyCollection<ICoordinate>> neighbourhood;
    }
}