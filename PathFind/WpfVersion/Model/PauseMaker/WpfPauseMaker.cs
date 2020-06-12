using System.Windows.Forms;

namespace WpfVersion.Model.PauseMaker
{
    public class WpfPauseMaker : GraphLibrary.PauseMaker.PauseMaker
    {       
        protected override void PauseEvent()
        {
            Application.DoEvents();
        }
    }
}
