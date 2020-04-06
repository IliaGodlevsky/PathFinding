using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphFactory;

namespace SearchAlgorythms.GraphLoader
{
    public class WinFormsGraphLoader : IGraphLoader
    {
        private readonly int placeBetweenButtons;
        private AbstractGraph graph;

        public WinFormsGraphLoader(int placeBetweenButtons)
        {
            this.placeBetweenButtons = placeBetweenButtons;
        }

        public AbstractGraph GetGraph()
        {
            VertexInfo[,] vertexInfo = null;
            var open = new OpenFileDialog();
            var formatter = new BinaryFormatter();
            if (open.ShowDialog() == DialogResult.OK)
                using (var stream = new FileStream(open.FileName, FileMode.Open))
                {
                    try
                    {
                        vertexInfo = (VertexInfo[,])formatter.Deserialize(stream);
                        Initialise(vertexInfo);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return null;
                    }
                }
            return graph;
        }

        private void Initialise(VertexInfo[,] info)
        {
            var creator = new WinFormsGraphInitializer(info, placeBetweenButtons);
            if (info == null)
                return;
            graph = (WinFormsGraph)creator.GetGraph();
            var setter = new NeigbourSetter(graph.GetArray());
            setter.SetNeighbours();
        }
    }
}
