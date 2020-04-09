using System.Windows.Forms;

namespace SearchAlgorythms.PauseMaker
{
    public class WinFormsPauseMaker : PauseMaker
    {
        protected override void PauseEvent()
        {
            Application.DoEvents();
        }
    }
}
