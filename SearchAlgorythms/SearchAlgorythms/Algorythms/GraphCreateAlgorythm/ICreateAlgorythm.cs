using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms.GraphCreateAlgorythm
{
    public interface ICreateAlgorythm
    {
        Button[,] GetGraph(int x, int y);
        bool IsObstacleChance();
    }
}
