using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfVersion.Extensions
{
    public static class ImageExtensions
    {
        public static Image SetImage(this Image self, Uri uri, string resourceName)
        {
            self.Source = new BitmapImage(uri);
            self.Style = (Style)self.TryFindResource(resourceName);
            return self;
        }
    }
}
