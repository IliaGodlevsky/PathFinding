using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using SearchAlgorythms.Graph;

namespace SearchAlgorythms.GraphSaver
{
    public class ButtonGraphSaver : IGraphSaver
    {
        public void SaveGraph(AbstractGraph graph)
        {
            if (graph != null)
            {
                SaveFileDialog save = new SaveFileDialog();
                GraphTopInfo[,] info = graph.Info;
                BinaryFormatter f = new BinaryFormatter();
                if (save.ShowDialog() == DialogResult.OK)
                {
                    using (var stream = new FileStream(save.FileName, FileMode.Create))
                    {
                        try
                        {
                            f.Serialize(stream, info);
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
