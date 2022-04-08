﻿using System.Windows.Media;
using WPFVersion3D.Model;
using WPFVersion3D.ViewModel.BaseViewModel;

namespace WPFVersion3D.ViewModel.VertexOpacityViewModels
{
    internal class EnqueuedVertexOpacityViewModel : BaseVertexOpacityViewModel
    {
        protected override Brush Color => VertexVisualization.EnqueuedVertexBrush;
    }
}