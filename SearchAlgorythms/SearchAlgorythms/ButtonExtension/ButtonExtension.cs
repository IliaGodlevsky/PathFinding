using SearchAlgorythms.Top;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.ButtonExtension
{
    public static class ButtonExtension
    {
        public static bool IsObstacle(this Button button)
        {
            return button as GraphTop == null;
        }

        public static GraphTopInfo GetInfo(this Button button)
        {
            return new GraphTopInfo(button);
        }

        public static void MarkAsVisited(this Button button)
        {
            button.BackColor = Color.FromName("Yellow");
        }

        public static void MarkAsPath(this Button button)
        {
            button.BackColor = Color.FromName("Cyan");
        }

        public static void MarkAsObstacle(this Button button)
        {
            button.BackColor = Color.FromName("Black");
        }

        public static void MarkAsStart(this Button button)
        {
            button.BackColor = Color.FromName("Green");
        }

        public static void MarkAsEnd(this Button button)
        {
            button.BackColor = Color.FromName("Red");
        }
    }
}
