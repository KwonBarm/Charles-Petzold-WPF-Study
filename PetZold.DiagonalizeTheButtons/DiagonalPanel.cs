using System.Windows;
using System.Windows.Media;

namespace PetZold.DiagonalizeTheButtons
{
    internal class DiagonalPanel : FrameworkElement
    {
        // private children 컬렉션
        List<UIElement> children = new List<UIElement>();

        // Private 필드
        Size sizeChildrenTotal;

        // 의존성 속성
        public static readonly DependencyProperty BackgroundProperty;

        // 정적 생성자에서 Background 의존성 속성 등록
        static DiagonalPanel()
        {
            BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(DiagonalPanel),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        // Background 프로퍼티
        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        // 자식 컬렉션에 접근하는 메서드
        public void Add(UIElement el)
        {
            children.Add(el);
            AddVisualChild(el);
            AddLogicalChild(el);
            InvalidateMeasure();
        }

        public void Remove(UIElement el)
        {
            children.Remove(el);
            RemoveVisualChild(el);
            RemoveLogicalChild(el);
            InvalidateMeasure();
        }

        public int Indexof(UIElement el)
        {
            return children.IndexOf(el);
        }

        // 오버라이딩 하는 프로퍼티와 메소드
        protected override int VisualChildrenCount
        {
            get { return children.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= children.Count)
                throw new ArgumentOutOfRangeException("index");

            return children[index];
        }

        protected override Size MeasureOverride(Size sizeAvailable)
        {
            sizeChildrenTotal = new Size(0, 0);

            foreach (UIElement child in children)
            {
                // 각 자식에 대해 Measure를 호출하고 총 크기를 계산
                child.Measure(sizeAvailable);
                sizeChildrenTotal.Width += child.DesiredSize.Width;
                sizeChildrenTotal.Height += child.DesiredSize.Height;
            }

            return sizeChildrenTotal;
        }

        protected override Size ArrangeOverride(Size sizeFinal)
        {
            Point ptChild = new Point(0, 0);

            foreach (UIElement child in children)
            {
                Size sizeChild = new Size(0, 0);
                sizeChild.Width = child.DesiredSize.Width * (sizeFinal.Width / sizeChildrenTotal.Width);
                sizeChild.Height = child.DesiredSize.Height * (sizeFinal.Height / sizeChildrenTotal.Height);

                child.Arrange(new Rect(ptChild, sizeChild));

                ptChild.X += sizeChild.Width;
                ptChild.Y += sizeChild.Height;
            }

            return sizeFinal;
        }

        protected override void OnRender(DrawingContext dc)
        {
            dc.DrawRectangle(Background, null, new Rect(new Point(0, 0), RenderSize));
        }
    }
}
