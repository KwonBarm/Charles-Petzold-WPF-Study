using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.CalculateInHex
{
    internal class RoundedButton : Control
    {
        // Private fields
        RoundedButtonDecorator decorator;

        // Public 정적 ClickEvent
        public static readonly RoutedEvent ClickEvent;

        // 정적 생성자
        // RoutedEvent ClickEvent를 등록
        // 의존성 속성을 등록하는 것과 비슷하게 RoutedEvent를 등록함
        static RoundedButton()
        {
            ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RoundedButton));
        }

        // 생성자
        // RounedButton 생성시에 RoundedButtonDecorator를 생성하여 자식으로 설정
        // 시각적, 논리적 자식으로 추가
        public RoundedButton()
        {
            decorator = new RoundedButtonDecorator();
            AddVisualChild(decorator);
            AddLogicalChild(decorator);
        }

        // public 속성
        //RoundedButton의 Child 속성은 RoundedButtonDecorator의 Child 속성과 같음
        public UIElement Child
        {
            get { return decorator.Child; }
            set { decorator.Child = value; }
        }

        //RoundedButton의 IsPressed 속성은 RoundedButtonDecorator의 IsPressed 속성과 같음
        public bool IsPressed
        {
            get { return decorator.IsPressed; }
            set { decorator.IsPressed = value; }
        }

        // Public 이벤트
        // RouteEventHandler라는 델리게이트를 사용하여 이름이 Click인 이벤트를 정의
        // 이벤트를 추가, 제거 가능하도록 add, remove 접근자를 정의
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        // 오버라이딩하는 프로퍼티와 메소드
        // Contorl은 하나의 자식만 가질 수 있으므로 VisualChildrenCount는 1
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        // VisualChildrenCount가 1이 아닌 경우 ArgumentOutOfRangeException 예외를 발생시킴
        protected override Visual GetVisualChild(int index)
        {
            if (index > 0)
                throw new ArgumentOutOfRangeException("index");

            return decorator;
        }

        // RoundedButton의 사이즈는 RoundedButtonDecorator의 사이즈와 같음
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            decorator.Measure(sizeAvailable);
            return decorator.DesiredSize;
        }

        // RoundedButton의 배치는 RoundedButtonDecorator의 배치와 같음
        protected override Size ArrangeOverride(Size sizeArrange)
        {
            decorator.Arrange(new Rect(new Point(0, 0), sizeArrange));
            return sizeArrange;
        }

        // 마우스가 버튼 위에 있고 IsMouseCaptured가 true일 때만 IsPressed에 IsMouseReallyOver를 할당
        // IsMouseReallyOver는 마우스 버튼 위에 있을  때 true를 반환
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if(IsMouseCaptured)
                IsPressed = IsMouseReallyOver;
        }

        // 마우스 왼쪽 버튼을 누르면 마우스 캡쳐하고 IsPressed를 true로 설정
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            CaptureMouse();
            IsPressed = true;
            e.Handled = true;
        }

        // 마우스 왼쪽 버튼을 떼면 마우스를 캡처하고 있고 마우스가 버튼 위에 있으면 OnClick 메소드를 호출
        // 마우스 캡처를 해제하고 IsPressed를 false로 설정
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (IsMouseCaptured)
            {
                if (IsMouseReallyOver)
                    OnClick();

                Mouse.Capture(null);
                IsPressed = false;
                e.Handled = true;
            }
        }

        // 마우스의 위치가 RoundedButton 위에 있는지 확인하는 IsMouseReallyOver 속성
        bool IsMouseReallyOver
        {
            get
            {
                Point pt = Mouse.GetPosition(this);
                return (pt.X >= 0) && (pt.X < ActualWidth) && (pt.Y >= 0) && (pt.Y < ActualHeight);
            }
        }

        // Click 이벤트를 발생시키는 OnClick 메소드
        // 해당 이벤트는 마우스 왼쪽 버튼을 떼었을 때 그리고 마우스가 캡처되어 있을 때 발생
        // RoutedEventArgs하고 RoutedEvent 속성을 ClickEvent로 설정
        // Source 속성을 자신으로 설정
        // RaiseEvent 메소드를 호출하여 이벤트를 발생시킴

        protected virtual void OnClick()
        {
            RoutedEventArgs argsEvent = new RoutedEventArgs();
            argsEvent.RoutedEvent = RoundedButton.ClickEvent;
            argsEvent.Source = this;
            RaiseEvent(argsEvent);
        }
    }
}
