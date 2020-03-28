using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SearchAlgorithms.Graph;

namespace SearchAlgorithms.GraphSaver
{
    public class ButtonGraphSaver : IGraphSaver
    {
        public void SaveGraph(IGraph graph)
        {
            if (graph != null)
            {
                SaveFileDialog save = new SaveFileDialog();
                IGraphTopInfo[,] info = graph.GetInfo();
                BinaryFormatter f = new BinaryFormatter();
                if (save.ShowDialog() == DialogResult.OK)
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
