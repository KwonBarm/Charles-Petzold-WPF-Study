using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.CircleTheButtons
{
    internal class RadialPanel : Panel
    {
        // 의존성 속성
        public static readonly DependencyProperty OrientationProperty;

        // Private 필드
        bool sholPieLines;
        double angleEach;
        Size sizeLargest;
        double radius;
        double outerEdgeFromCenter;
        double innerEdgeFromCenter;

        // 정적 생성자에서 OrientationProperty 초기화
        static RadialPanel()
        {
            OrientationProperty = DependencyProperty.Register("Orientation",
                typeof(RadialPanelOrientation),
                typeof(RadialPanel),
                new FrameworkPropertyMetadata(RadialPanelOrientation.ByWidth,
                FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        // Orientation 속성
        public RadialPanelOrientation Orientation
        {
            get { return (RadialPanelOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // showPieLines 속성
        public bool ShowPieLines
        {
            set
            {
                if (value != sholPieLines)
                    InvalidateVisual();

                sholPieLines = value;
            }
            get
            {
                return sholPieLines;
            }
        }

        // MeasureOverride 오버라이딩
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            if (InternalChildren.Count == 0)
                return new Size(0, 0);

            angleEach = 360 / InternalChildren.Count;
            sizeLargest = new Size(0, 0);

            foreach (UIElement child in InternalChildren)
            {
                // 각 자식에 대해 Measure 호출
                child.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

                // 그리고 각 자식의 DesiredSize 속성을 활용
                sizeLargest.Width = Math.Max(sizeLargest.Width, child.DesiredSize.Width);
                sizeLargest.Height = Math.Max(sizeLargest.Height, child.DesiredSize.Height);
            }

            if (Orientation == RadialPanelOrientation.ByWidth)
            {
                // 중심에서 엘리먼트 변까지의 거리를 계산
                innerEdgeFromCenter = sizeLargest.Width / 2 / Math.Tan(Math.PI * angleEach / 360);
                outerEdgeFromCenter = innerEdgeFromCenter + sizeLargest.Height;

                // 가장 큰 자식을 기준으로 원의 반지름을 계산
                radius = Math.Sqrt(Math.Pow(sizeLargest.Width / 2, 2) + Math.Pow(outerEdgeFromCenter, 2));
            }
            else
            {
                // 중심에서 엘리멘트 변까지의 거리를 계산
                innerEdgeFromCenter = sizeLargest.Height / 2 / Math.Tan(Math.PI * angleEach / 360);
                outerEdgeFromCenter = innerEdgeFromCenter + sizeLargest.Width;
            }

            // 원의 크기를 반환
            return new Size(2 * radius, 2 * radius);
        }

        // ArrangeOverrid 오버라이딩
        protected override Size ArrangeOverride(Size sizeFianl)
        {
            double angleChild = 0;
            Point ptCenter = new Point(sizeFianl.Width / 2, sizeFianl.Height / 2);
            double multiplier = Math.Min(sizeFianl.Width / (2 * radius), sizeFianl.Height / (2 * radius));

            foreach (UIElement child in InternalChildren)
            {
                // RenderTransform을 리셋
                child.RenderTransform = Transform.Identity;

                if (Orientation == RadialPanelOrientation.ByWidth)
                {
                    // 상단에 자식을 위치
                    child.Arrange(new Rect(ptCenter.X - multiplier * sizeLargest.Width / 2, ptCenter.Y - multiplier * outerEdgeFromCenter, multiplier * sizeLargest.Width, multiplier * sizeLargest.Height));
                }
                else
                {
                    // 오른쪽에 자식을 배치
                    child.Arrange(new Rect(ptCenter.X + multiplier * innerEdgeFromCenter,ptCenter.Y - multiplier * sizeLargest.Height/2, multiplier*sizeLargest.Width, multiplier * sizeLargest.Height));
                }

                // 원 주위로 자식을 회전(자식에 대해 상대적)
                Point pt = TranslatePoint(ptCenter, child);
                child.RenderTransform = new RotateTransform(angleChild, pt.X, pt.Y);

                // 각도 증가
                angleChild += angleEach;
            }

            return sizeFianl;
        }

        // OnRender 오버라이딩, 선택 사항인 선을 출력
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            if (ShowPieLines)
            {
                Point ptCenter = new Point(RenderSize.Width / 2, RenderSize.Height / 2);
                double multiplier = Math.Min(RenderSize.Width / (2 * radius), RenderSize.Height / (2 * radius));
                Pen pen = new Pen(SystemColors.WindowTextBrush, 1);
                pen.DashStyle = DashStyles.Dash;

                //원을 출력
                dc.DrawEllipse(null, pen, ptCenter, multiplier * radius, multiplier * radius);

                //각도를 초기화
                double angleChild = -angleEach / 2;

                if (Orientation == RadialPanelOrientation.ByWidth)
                {
                    angleChild += 90;
                }

                // 각 자식에 대해 중심에서부터 방사 형태의 선을 출력하는 루프
                foreach (UIElement child in InternalChildren)
                {
                    dc.DrawLine(pen, ptCenter, new Point(ptCenter.X + multiplier * radius * Math.Cos(2 * Math.PI * angleChild / 360),
                        ptCenter.Y + multiplier * radius * Math.Sin(2 * Math.PI * angleChild / 360)));
                    angleChild += angleEach;
                }
            }
        }
    }
}

