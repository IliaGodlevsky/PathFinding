using GraphLibrary;
using GraphLibrary.GraphFactory;
using GraphLibrary.GraphLoader;
using System;

namespace WpfVersion.Model.GraphLoader
{
    public class WpfGraphLoader : AbstractGraphLoader
    {
        protected override AbstractGraphInitializer GetInitializer(VertexInfo[,] info)
        {
            throw new NotImplementedException();
        }

        protected override string GetPath()
        {
            throw new NotImplementedException();
        }

        protected override void ShowMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
