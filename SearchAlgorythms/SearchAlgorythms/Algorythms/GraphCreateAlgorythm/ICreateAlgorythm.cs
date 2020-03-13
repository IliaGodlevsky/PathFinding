using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms.GraphCreateAlgorythm
{
    public interface ICreateAlgorythm
    {
        Button[,] GetGraph(int x, int y);
        void SetNeighbours(int x, int y);
    }
}
