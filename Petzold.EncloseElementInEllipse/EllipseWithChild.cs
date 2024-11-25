using System.Windows;
using System.Windows.Media;

namespace Petzold.EncloseElementInEllipse
{
    class EllipseWithChild : Petzold.RenderTheBetterEllipse.BetterEllipse
    {
        UIElement child;

        // Public Child 프로퍼티
        public UIElement Child
        {
            set
            {
                if (child != null)
                {
                    RemoveVisualChild(child);
                    RemoveLogicalChild(child);
                }
                if ((child = value) != null)
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

        // VisualChildrenCount 오버라이딩. Child가 null이 아니면 1을 반환
        protected override int VisualChildrenCount
        {
            get { return child != null ? 1 : 0; }
        }

        // GetVisualChildren 오버라이딩. Child를 반환,,,,  Panel이 아니기 때문에 하나의 자식요소만 가질 수 있음, 그게 인덱스 0
        protected override Visual GetVisualChild(int index)
        {
            if (index > 0 || Child == null)
                throw new ArgumentOutOfRangeException("index");

            return child;
        }

        // MeasureOverride 오버라이딩. Child의 Measure 메소드를 호출 
        // Size sizeAvailable : 부모 요소가 제공한 사이즈
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            Size sizeDesired = new Size(0, 0);
            // 최소 요구 사이즈
            if (Stroke != null)
            {
                sizeDesired.Width += 2 * Stroke.Thickness;
                sizeDesired.Height += 2 * Stroke.Thickness;
            }

            // 자식 요소의 최소 요구 사이즈
            sizeAvailable.Width = Math.Max(0, sizeAvailable.Width - 2 * Stroke.Thickness);
            sizeAvailable.Height = Math.Max(0, sizeAvailable.Height - 2 * Stroke.Thickness);

            if (Child != null)
            {
                Child.Measure(sizeAvailable);

                sizeDesired.Width += Child.DesiredSize.Width;
                sizeDesired.Height += Child.DesiredSize.Height;
            }

            return sizeDesired;
        }

        // ArrangeOverride 오버라이딩. Child의 Arrange 메소드를 호출
        // Size sizeFinal : 부모 요소가 할당한 최종 사이즈 결론적으로 sizeDesired값 전달
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            if (Child != null)
            {
                Rect rect = new Rect(new Point((sizeFinal.Width - Child.DesiredSize.Width) / 2, (sizeFinal.Height - Child.DesiredSize.Height) / 2), Child.DesiredSize);
                Child.Arrange(rect);
            }

            return sizeFinal;
        }
    }
}
