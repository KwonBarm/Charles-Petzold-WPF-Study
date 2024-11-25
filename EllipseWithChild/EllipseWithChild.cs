using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Petzold.EncloseElementInEllipse
{
    class EllipseWithChild : Petzold.RenderTheBetterEllips.BetterEllipse
    {
        UIElement child;

        // Public Child 속성
        public UIElement Child
        {
            set
            {
                if (child != null)
                {
                    RemoveVisualChild(child);
                    RemoveLogicalChild(child);
                }
                if ((child = value) != null) // child에 value 할당, chile이 null이 아니면 실행
                {
                    AddVisualChild(child);
                    AddLogicalChild(child);
                }
            }
            get
            {
                return child;
            }
        }

        // VisualChildrenCount 오버라이딩, Child가 null이 아니면 1 반환
        protected override int VisualChildrenCount
        {
            get
            {
                return Child != null ? 1 : 0;
            }
        }

        // GetVisualChild 오버라이딩, Child를 반환
        protected override Visual GetVisualChild(int index)
        {
            if (index > 0 || Child == null)
                throw new ArgumentOutOfRangeException("index");

            return Child;
        }

        protected override Size MeasureOverride(Size sizeAvailable)
        {
            Size sizeDesired = new Size(0, 0);
            if (Stroke != null)
            {
                sizeDesired.Width += 2 * Stroke.Thickness;
                sizeDesired.Height += 2 * Stroke.Thickness;

                sizeAvailable.Width = Math.Max(0, sizeAvailable.Width - 2*Stroke.Thickness);
                sizeAvailable.Height = Math.Max(0, sizeAvailable.Height - 2*Stroke.Thickness);
                
                if(Child != null)
                {
                    Child.Measure(sizeDesired);

                    sizeDesired.Width += Child.DesiredSize.Height;
                    sizeDesired.Height += Child.DesiredSize.Height;
                }
            }

            return sizeDesired;
        }
    }
}
