using SearchAlgorythms.Top;
using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms.GraphCreateAlgorythm
{
    public interface ICreateAlgorythm
    {
        IGraphTop[,] GetGraph();
    }
}
