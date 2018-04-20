using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using IAmProductive.iOS.Renderers;
using IAmProductive.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]

namespace IAmProductive.iOS.Renderers
{
    public class CustomFrameRenderer : FrameRenderer
    {
        public CustomFrameRenderer()
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            Layer.BorderColor = UIColor.White.CGColor;
            Layer.CornerRadius = 10;
            Layer.MasksToBounds = false;
            Layer.ShadowOffset = new CGSize(-2, 2);
            Layer.ShadowRadius = 5;
            Layer.ShadowOpacity = 0.4f;
        }
    }
}