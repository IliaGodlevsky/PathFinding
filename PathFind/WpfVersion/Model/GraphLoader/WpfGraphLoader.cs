using GraphLibrary.DTO;
using GraphLibrary.GraphFactory;
using GraphLibrary.GraphSerialization.GraphLoader;
using WpfVersion.Model.GraphFactory;

namespace WpfVersion.Model.GraphLoader
{
    internal class WpfGraphLoader : AbstractGraphLoader
    {
        private readonly int placeBetweenButtons;

        public WpfGraphLoader(int placeBetweenButtons) => this.placeBetweenButtons = placeBetweenButtons;

        protected override AbstractGraphInfoInitializer GetInitializer(VertexInfo[,] info)
            => new WpfGraphInitializer(info, placeBetweenButtons);
    }
}
