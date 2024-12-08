using System.Windows;
using System.Windows.Media;

namespace Petzold.SelectColor
{
    internal class ColorCell : FrameworkElement
    {
        // private 필드
        static readonly Size sizeCell = new Size(20, 20);
        DrawingVisual visColor;
        Brush brush;

        // 의존 프로퍼티
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty IsHighlightProperty;

        static ColorCell()
        {
            IsSelectedProperty
                = DependencyProperty.Register("IsSelected", typeof(bool), typeof(ColorCell), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
            IsHighlightProperty
                = DependencyProperty.Register("IsHighlight", typeof(bool), typeof(ColorCell), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        // 프로퍼티
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public bool IsHighlighted
        {
            get { return (bool)GetValue(IsHighlightProperty); }
            set { SetValue(IsHighlightProperty, value); }
        }

        public Brush Brush
        {
            get { return brush; }
        }

        // Color을 인자로 하는 생성자
        public ColorCell(Color clr)
        {
            // DrawingVisual을 생성해 필드에 저장
            visColor = new DrawingVisual();
            DrawingContext dc = visColor.RenderOpen();

            // Color 인자를 가지고 사각형 그리기
            Rect rect = new Rect(new Point(0, 0), sizeCell);
            rect.Inflate(-4, -4);
            Pen pen = new Pen(SystemColors.ControlTextBrush, 1);
            brush = new SolidColorBrush(clr);
            dc.DrawRectangle(brush, pen, rect);
            dc.Close();

            // AddvisualChild는 이벤트 라우팅에 필요함!
            AddVisualChild(visColor);
            AddLogicalChild(visColor);
        }
        // VisualChild에 관련된 protected 프로퍼티와 메소드를 오버라이딩
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index > 0)
                throw new ArgumentOutOfRangeException("index");

            return visColor;
        }

        // 엘리멘트의 크기와 렌더링과 관련된 protected 메소드를 오버라이딩
        protected override Size MeasureOverride(Size availableSize)
        {
            return sizeCell;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Rect rect = new Rect(new Point(0, 0), RenderSize);
            rect.Inflate(-1, -1);
            Pen pen = new Pen(SystemColors.HighlightBrush, 1);

            if(IsHighlighted)
                drawingContext.DrawRectangle(SystemColors.ControlDarkBrush, pen, rect);
            else if (IsSelected)
                drawingContext.DrawRectangle(SystemColors.ControlLightBrush, pen, rect);
            else
                drawingContext.DrawRectangle(Brushes.Transparent, null, rect);
        }
    }
}

