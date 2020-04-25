using System.Windows.Forms;

namespace WinFormsVersion.PauseMaker
{
    public class WinFormsPauseMaker : GraphLibrary.PauseMaker.PauseMaker
    {
        protected override void PauseEvent()
        {
            Application.DoEvents();
        }
    }
}
