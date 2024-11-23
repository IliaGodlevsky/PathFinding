using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
    internal record IsExternalInit;
}

namespace Pathfinding.Service.Interface.Models.Undefined
{
    public record GraphAssembleModel(IReadOnlyList<int> Dimensions,
        IReadOnlyCollection<VertexAssembleModel> Vertices);
}
