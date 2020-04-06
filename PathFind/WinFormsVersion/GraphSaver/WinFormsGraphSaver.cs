using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SearchAlgorythms.Graph;

namespace SearchAlgorythms.GraphSaver
{
    public class WinFormsGraphSaver : IGraphSaver
    {
        public void SaveGraph(AbstractGraph graph)
        {
            if (graph != null)
            {
                var save = new SaveFileDialog();
                var vertexInfo = graph.Info;
                var formatter = new BinaryFormatter();
                if (save.ShowDialog() == DialogResult.OK)
                {
                    using (var stream = new FileStream(save.FileName, FileMode.Create))
                    {
                        try
                        {
                            formatter.Serialize(stream, vertexInfo);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                }
            }
        }
    }
}
