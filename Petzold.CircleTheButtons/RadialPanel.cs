using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

            foreach(UIElement child in InternalChildren)
            {
                // 각 자식에 대해 Measure 호출
                child.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

                // 그리고 각 자식의 DesiredSize 속성을 활용
                sizeLargest.Width = Math.Max(sizeLargest.Width, child.DesiredSize.Width);
                sizeLargest.Height = Math.Max(sizeLargest.Height, child.DesiredSize.Height);
            }

            if(Orientation == RadialPanelOrientation.ByWidth)
            {
                // 중심에서 엘리먼트 변까지의 거리를 계산
                innerEdgeFromCenter = sizeLargest.Width / 2/ Math.Tan(Math.PI * angleEach / 360);
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
    }
}

