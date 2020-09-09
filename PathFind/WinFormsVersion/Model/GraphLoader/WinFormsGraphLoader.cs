using GraphLibrary.DTO;
using GraphLibrary.GraphFactory;
using GraphLibrary.GraphSerialization.GraphLoader;
using WinFormsVersion.GraphFactory;

namespace WinFormsVersion.GraphLoader
{
    internal class WinFormsGraphLoader : AbstractGraphLoader
    {
        private readonly int placeBetweenButtons;

        public WinFormsGraphLoader(int placeBetweenButtons) => this.placeBetweenButtons = placeBetweenButtons;

        protected override AbstractGraphInfoInitializer GetInitializer(VertexDto[,] info) 
            => new WinFormsGraphInitializer(info, placeBetweenButtons);
    }
}
