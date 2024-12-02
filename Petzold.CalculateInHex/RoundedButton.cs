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
        static RoundedButton()
        {
            ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RoundedButton));
        }

        // 생성자
        public RoundedButton()
        {
            decorator = new RoundedButtonDecorator();
            AddVisualChild(decorator);
            AddLogicalChild(decorator);
        }

        // public 속성
        public UIElement Child
        {
            get { return decorator.Child; }
            set { decorator.Child = value; }
        }

        public bool IsPressed
        {
            get { return decorator.IsPressed; }
            set { decorator.IsPressed = value; }
        }

        // Public 이벤트
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        // 오버라이딩하는 프로퍼티와 메소드
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }
        protected override Visual GetVisualChild(int index)
        {
            if (index > 0)
                throw new ArgumentOutOfRangeException("index");

            return decorator;
        }
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            decorator.Measure(sizeAvailable);
            return decorator.DesiredSize;
        }
        protected override Size ArrangeOverride(Size sizeArrange)
        {
            decorator.Arrange(new Rect(new Point(0, 0), sizeArrange));
            return sizeArrange;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if(IsMouseCaptured)
                IsPressed = IsMouseReallyOver;
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            CaptureMouse();
            IsPressed = true;
            e.Handled = true;
        }
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

        bool IsMouseReallyOver
        {
            get
            {
                Point pt = Mouse.GetPosition(this);
                return (pt.X >= 0) && (pt.X < ActualWidth) && (pt.Y >= 0) && (pt.Y < ActualHeight);
            }
        }

        // Click 이벤트를 발생시키는 OnClick 메소드
        protected virtual void OnClick()
        {
            RoutedEventArgs argsEvent = new RoutedEventArgs();
            argsEvent.RoutedEvent = RoundedButton.ClickEvent;
            argsEvent.Source = this;
            RaiseEvent(argsEvent);
        }
    }
}
