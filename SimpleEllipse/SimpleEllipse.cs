using System.Windows;
using System.Windows.Media;

namespace SimpleEllipse
{
    internal class SimpleEllipse : FrameworkElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawEllipse(Brushes.Blue, new Pen(Brushes.Red, 24), new Point(RenderSize.Width / 2, RenderSize.Height / 2), RenderSize.Width / 2, RenderSize.Height / 2);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size sizeDesired;

            return sizeDesired;
        }
    }
}
