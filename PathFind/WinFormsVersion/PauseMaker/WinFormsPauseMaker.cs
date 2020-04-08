using System.Windows.Forms;

namespace SearchAlgorythms.PauseMaker
{
    public class WinFormsPauseMaker : PauseMaker
    {
        public override void PauseEvent()
        {
            Application.DoEvents();
        }
    }
}
