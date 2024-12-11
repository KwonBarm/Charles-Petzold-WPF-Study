using System.Windows;
using System.Windows.Controls;

namespace Petzold.PaintOnCanvasClone
{
    internal class CanvasClone : Panel
    {
        // 2개의 의존성 속성
        public static readonly DependencyProperty LeftProperty;
        public static readonly DependencyProperty TopProperty;

        static CanvasClone()
        {
            // 읜존성 속성을 첨부 프로퍼티로 등록
            // 기본값은 0이며, 변경이 되면 부모의 배치에 영향을 줌
            LeftProperty = DependencyProperty.RegisterAttached("Left",typeof(double), typeof(CanvasClone),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsParentArrange));

            TopProperty = DependencyProperty.RegisterAttached("Top",typeof(double), typeof(CanvasClone),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsParentArrange));
        }

        // 첨부 프로퍼티를 얻어오고 설정하는 정적 메서드
        public static void SetLeft(DependencyObject obj, double value)
        {
            obj.SetValue(LeftProperty, value);
        }

        public static double GetLeft(DependencyObject obj)
        {
            return (double)obj.GetValue(LeftProperty);
        }

        public static void SetTop(DependencyObject obj, double value)
        {
            obj.SetValue(TopProperty, value);
        }

        public static double GetTop(DependencyObject obj)
        {
            return (double)obj.GetValue(TopProperty);
        }

        // MeasureOverride 메서드를 오버라이딩 에서는 자식의 Measure를 호출
        protected override Size MeasureOverride(Size sizeAvailble)
        {
            foreach (UIElement child in InternalChildren)
            {
                child.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            }

            // 기본값 (0,0)을 반환
            return base.MeasureOverride(sizeAvailble);
        }

        // ArrangeOverride 메서드를 오버라이딩하여 자식을 배치
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            foreach (UIElement child in InternalChildren)
            {
                child.Arrange(new Rect(new Point(GetLeft(child), GetTop(child)), child.DesiredSize));
            }

            return sizeFinal;
        }
    }
}
