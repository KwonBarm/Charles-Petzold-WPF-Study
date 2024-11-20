using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SimpleEllipse
{
    internal class SimpleEllipse : FrameworkElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawEllipse(Brushes.Red, new Pen(Brushes.Blue, 20), new Point(RenderSize.Width / 2, RenderSize.Height / 2),
                RenderSize.Width / 2, RenderSize.Height / 2);
        }
    }
}
