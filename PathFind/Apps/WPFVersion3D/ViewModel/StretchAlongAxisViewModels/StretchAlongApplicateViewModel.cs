﻿using WPFVersion3D.Interface;
using WPFVersion3D.ViewModel.BaseViewModel;

namespace WPFVersion3D.ViewModel.StretchAlongAxisViewModels
{
    internal sealed class StretchAlongApplicateViewModel : BaseStretchAlongAxisViewModel
    {
        protected override IAxis Axis => graphField?.Applicate;
    }
}