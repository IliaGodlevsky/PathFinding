using CommunityToolkit.Mvvm.Messaging;
using MimeKit.Cryptography;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Serialization;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Import
{
    [HighPriority]
    internal sealed class LoadGraphFromFileMenuItem(IMessenger messenger,
        IFilePathInput input,
        ISerializer<IEnumerable<GraphSerializationDto>> serializer,
        ILog log,
        IService<Vertex> service) : ImportGraphFromFileMenuItem<GraphSerializationDto>(messenger, input, serializer, log, service)
    {
        protected override void AddImported(IEnumerable<GraphSerializationDto> imported)
        {
            imported.ForEach(x => service.AddGraph(x));
        }

        protected override GraphReadDto<Vertex> AddSingleImported(GraphSerializationDto imported)
        {
            return service.AddGraph(imported);
        }

        public override string ToString() => Languages.LoadGraph;

        protected override void Post(GraphReadDto<Vertex> graph)
        {
           
        }
    }
}
