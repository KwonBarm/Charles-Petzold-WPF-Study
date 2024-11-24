using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GetMedieval
{
    class MedievalButton : Control
    {
        // 2개의 private 필드
        FormattedText formtxt;
        bool isMouseReallyOver;

        // 정적 읽기 전용 필드
        public static readonly DependencyProperty TextProperty;
        public static readonly RoutedEvent KnockEvent;
        public static readonly RoutedEvent PreviewKnockEvent;

        // 정적 생성자
        static MedievalButton()
        {
            // 의존성 프로퍼티 등록
            TextProperty = DependencyProperty.Register
                (
                "Text",
                typeof(string),
                typeof(MedievalButton),
                new FrameworkPropertyMetadata(" ", FrameworkPropertyMetadataOptions.AffectsMeasure)
                );

            // 라우팅 이벤트 등록
            KnockEvent = EventManager.RegisterRoutedEvent
                (
                "Knonk",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(MedievalButton)
                );

            PreviewKnockEvent = EventManager.RegisterRoutedEvent
                (
                "PreviewKnock",
                RoutingStrategy.Tunnel,
                typeof(RoutedEventHandler),
                typeof(MedievalButton)
                );
        }

        // 의존성 프로퍼티의 Public 인터페이스
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // 라우팅 이벤트의 Public 인터페이스
        public event RoutedEventHandler Knock
        {
            add { AddHandler(KnockEvent, value); }
            remove { RemoveHandler(KnockEvent, value); }
        }

        public event RoutedEventHandler PreviewKnock
        {
            add { AddHandler(PreviewKnockEvent, value); }
            remove { RemoveHandler(PreviewKnockEvent, value); }
        }

        // 레이아웃 시스템에 의해 자동 호출
        protected override Size MeasureOverride(Size constraint)
        {
            formtxt = new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection, new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Foreground);

            // 크기를 계산할때 Padding을 사용
            Size sizeDesire = new Size(Math.Max(48, formtxt.Width) + 4, formtxt.Height + 4);
            sizeDesire.Width += Padding.Left + Padding.Right;
            sizeDesire.Height += Padding.Top + Padding.Bottom;
            //sizeDesire.Width = Math.Min(sizeDesire.Width, constraint.Width);
            //sizeDesire.Height = Math.Min(sizeDesire.Height, constraint.Height);

            return sizeDesire;
        }

        // 버튼을 다시 그리기 위해 OnRender 호출
        protected override void OnRender(DrawingContext drawingContext)
        {
            Brush brushBackGround = SystemColors.ControlBrush;

            if (isMouseReallyOver && IsMouseCaptured)
                brushBackGround = SystemColors.ControlDarkBrush;

            // 펜의 두께 결정
            Pen pen = new Pen(Foreground, IsMouseOver ? 2 : 1);

            // 둥근 모서리의 사각형을 그림
            drawingContext.DrawRoundedRectangle(brushBackGround, pen, new Rect(new Point(0, 0), RenderSize), 4, 4);

            // 전경색 결정
            formtxt.SetForegroundBrush(IsEnabled ? Foreground : SystemColors.ControlDarkBrush);

            // 텍스트의 시작점 결정
            Point ptText = new Point(2, 2);

            switch (HorizontalContentAlignment)
            {
                case HorizontalAlignment.Left:
                    ptText.X += Padding.Left;
                    break;
                case HorizontalAlignment.Right:
                    ptText.X += RenderSize.Width - formtxt.Width - Padding.Right;
                    break;
                case HorizontalAlignment.Center:
                case HorizontalAlignment.Stretch:
                    ptText.X += (RenderSize.Width - formtxt.Width - Padding.Left - Padding.Right) / 2;
                    break;
            }

            switch (VerticalContentAlignment)
            {
                case VerticalAlignment.Top:
                    ptText.Y += Padding.Top;
                    break;
                case VerticalAlignment.Bottom:
                    ptText.Y += RenderSize.Height - formtxt.Height - Padding.Bottom;
                    break;
                case VerticalAlignment.Center:
                case VerticalAlignment.Stretch:
                    ptText.Y += (RenderSize.Height - formtxt.Height - Padding.Top - Padding.Bottom) / 2;
                    break;
            }

            // 텍스트 출력
            drawingContext.DrawText(formtxt, ptText);
        }

        // 버튼의 모습에 영향을 주는 Mouse 이벤트
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            InvalidateVisual(); // 렌더링을 강제로 다시 수행
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            InvalidateVisual();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // Determin if mouse has really moved inside or out.

            Point pt = e.GetPosition(this);
            bool isReallyOverNow = (pt.X >= 0 && pt.X < ActualWidth && pt.Y >= 0 && pt.Y < ActualHeight);

            if (isReallyOverNow != isMouseReallyOver)
            {
                isMouseReallyOver = isReallyOverNow;
                InvalidateVisual();
            }
        }

        // 'Knock' 이벤트를 발생시키는 작업의 시작
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            CaptureMouse(); // 이 메서드를 호출하면 마우스가 현재 요소의 범위를 벗어나더라도, 해당 요소가 계속해서 마우스 이벤트를 받을 수 있도록 설정
            InvalidateVisual();
            e.Handled = true;
        }

        // 이 이벤트가 실제로 'Knock' 이벤트 발생
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (IsMouseCaptured)
            {
                if (isMouseReallyOver)
                {
                    OnPreviewKnock();
                    OnKnock();
                }
                e.Handled = true;
                Mouse.Capture(null); // CaptureMouse()한 요소 해제
            }
        }

        // 마우스 캡처가 종료되면 다시 그림
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            InvalidateVisual();
        }

        // Space bar 또는 Enter 키를 눌러도 버튼을 누르는 효과
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Space || e.Key == Key.Enter)
                e.Handled = true;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                OnPreviewKnock();
                OnKnock();
                e.Handled = true;
            }
        }

        // OnKnock 메소드에서 'Knock' 이벤트를 발생
        protected virtual void OnKnock()
        {
            RoutedEventArgs argsEvent = new RoutedEventArgs();
            argsEvent.RoutedEvent = MedievalButton.KnockEvent;
            argsEvent.Source = this;
            RaiseEvent(argsEvent);
        }

        // OnPreviewKnock 메소드에서 'PreviewKnock' 이벤트를 발생
        protected virtual void OnPreviewKnock()
        {
            RoutedEventArgs argsEvent = new RoutedEventArgs();
            argsEvent.RoutedEvent = MedievalButton.PreviewKnockEvent;
            argsEvent.Source = this;
            RaiseEvent(argsEvent);
        }
    }
}
