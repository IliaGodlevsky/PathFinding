using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Import
{
    internal abstract class ImportGraphFromFileMenuItem<TImport>
        : ImportGraphMenuItem<string, TImport>
    {
        protected ImportGraphFromFileMenuItem(IMessenger messenger,
            IFilePathInput input,
            ISerializer<IEnumerable<TImport>> serializer,
            ILog log,
            IService service) : base(messenger, input, serializer, log, service)
        {
        }

        protected override sealed IReadOnlyCollection<TImport> ImportGraph(string path)
        {
            return serializer.DeserializeFromFile(path).ToReadOnly();
        }

        protected override sealed string InputPath()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                return input.Input("Enter path: ");
            }
        }
    }
}
