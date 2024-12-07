using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.CalculateInHex
{
    internal class RoundedButtonDecorator : Decorator
    {
        // Public 의존성 속성 필드
        public static readonly DependencyProperty IsPressedProperty;

        // 정적 생성자에서 의존성 속성 등록
        static RoundedButtonDecorator()
        {
            IsPressedProperty
                = DependencyProperty.Register("IsPressed", typeof(bool), typeof(RoundedButtonDecorator),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        // RoundedButtonDecorator의 Public 속성 정의
        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            set { SetValue(IsPressedProperty, value); }
        }

        // MeasureOverride 오버라이드
        // sizeAvailable 매개변수는 부모 패널이 자식에게 할당할 수 있는 공간을 나타냄
        // 이 메소드는 자식의 크기를 결정하고, 부모 패널에게 필요한 크기를 알려줌
        // 부모 패널은 이 메소드가 반환하는 크기를 사용하여 자식을 배치함
        // 이 메소드는 자식의 크기를 결정하기 위해 Measure 메소드를 호출함
        // 자신의 기본사이즈만큼을 뺀 sizeAvailable을 자식에게 전달함
        // Child.Measure(sizeAvailable)을 호출하여 자식의 크기를 결정함
        // 자신의 사이즈에 자식의 사이즈를 더하여 부모 패널에게 필요한 크기를 알려줌
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            Size szDesired = new Size(2, 2);
            sizeAvailable.Width -= 2;
            sizeAvailable.Height -= 2;

            if (Child != null)
            {
                Child.Measure(sizeAvailable);
                szDesired.Width += Child.DesiredSize.Width;
                szDesired.Height += Child.DesiredSize.Height;
            }

            return szDesired;
        }

        // ArrangeOverride 오버라이드
        // sizeArrange 매개변수는 부모 패널이 자식을 배치할 수 있는 공간을 나타냄
        // 이 메소드는 자식을 배치하고, 부모 패널에게 자신의 사이즈를 알려줌
        // 이 메소드는 자식을 배치하기 위해 Arrange 메소드를 호출함
        // 자식의 위치를 결정하기 위해 sizeArrange와 자식의 사이즈를 사용함
        protected override Size ArrangeOverride(Size sizeArrange)
        {
            if (Child != null)
            {
                Point ptChild = new Point(Math.Max(0, (sizeArrange.Width - Child.DesiredSize.Width) / 2),
                    Math.Max(0, (sizeArrange.Height - Child.DesiredSize.Height) / 2));

                Child.Arrange(new Rect(ptChild, Child.DesiredSize));
            }

            return sizeArrange;
        }

        // OnRender 오버라이드
        // 이 메소드는 자신을 그리기 위해 호출됨
        // 방사형 그라데이션 브러쉬를 생성하고 이 브러쉬는 IsPressed 속성에 따라 다름
        // DrawRoundedRectangle 메소드를 호출하여 둥근 사각형을 그리면 사각형의 모서리는 RenderSize.Height / 2만큼 둥글어짐
        protected override void OnRender(DrawingContext dc)
        {
            RadialGradientBrush brush = new RadialGradientBrush(IsPressed ? SystemColors.ControlDarkColor : SystemColors.ControlLightLightColor, SystemColors.ControlColor);
            brush.GradientOrigin = IsPressed ? new Point(0.75, 0.75) : new Point(0.25, 0.25);

            dc.DrawRoundedRectangle(brush, new Pen(SystemColors.ControlDarkDarkBrush, 1), new Rect(new Point(0, 0), RenderSize), RenderSize.Height / 2, RenderSize.Height / 2);
        }
    }
}
