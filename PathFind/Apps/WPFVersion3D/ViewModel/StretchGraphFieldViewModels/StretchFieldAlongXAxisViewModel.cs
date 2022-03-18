using WPFVersion3D.Axes;
using WPFVersion3D.Enums;

namespace WPFVersion3D.ViewModel.StretchGraphFieldViewModels
{
    internal sealed class StretchFieldAlongXAxisViewModel : StretchFieldAlongAxisViewModel
    {
        public override string Title { get; }

        protected override double[] AdditionalOffset { get; }

        public StretchFieldAlongXAxisViewModel() 
            : base(new Abscissa(), MessageTokens.StretchAlongXAxisModel)
        {
            AdditionalOffset = new double[] { 0, 0, 1 };
            Title = "X axis";
        }
    }
}
