using System.Windows;
using System.Windows.Controls;

namespace Petzold.DupalicateUniformGrid
{
    internal class UniformGridAlmost : Panel
    {
        // Public 읽기 전용 정적 의존성 속성
        public static readonly DependencyProperty ColumnsProperty;

        // 정적 생성자에서 의존성 속성을 등록
        static UniformGridAlmost()
        {
            ColumnsProperty
                = DependencyProperty.Register("Columns", typeof(int), typeof(UniformGridAlmost),
                new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        // Columns 속성
        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        // 읽기 전용 Rows 속성
        public int Rows
        {
            get { return (InternalChildren.Count + Columns - 1) / Columns; }
        }

        // MeasureOverride 오버라이딩해서 공간을 할당
        protected override Size MeasureOverride(Size availableSize)
        {
            // 행과 열을 고려해서 자식의 크기를 계산
            Size sizeChild = new Size(availableSize.Width / Columns, availableSize.Height / Rows);

            // 최대 폭과 높이를 누적시키기 위한 변수
            double maxwidth = 0;
            double maxheight = 0;

            foreach (UIElement child in InternalChildren)
            {
                // 각 자식마다 Measure를 호출
                child.Measure(sizeChild);

                // 그리고 자식의 DesirreSize 프로퍼티를 이용
                maxwidth = Math.Max(maxwidth, child.DesiredSize.Width);
                maxheight = Math.Max(maxheight, child.DesiredSize.Height);
            }

            // 그리드 자체의 요구 크기를 계산 
            return new Size(maxwidth * Columns, maxheight * Rows);
        }

        // ArrangeOverride 오버라이딩해서 자식을 배치
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            // 행과 열을 고려해서 자식의 크기를 계산 
            Size sizeChild = new Size(sizeFinal.Width / Columns, sizeFinal.Height / Rows);

            for (int index = 0; index < InternalChildren.Count; index++)
            {
                int row = index / Columns;
                int col = index % Columns;

                // sizeFinal 내에서 각 자식의 사각형을 계산 
                Rect rectChild = new Rect(new Point(col* sizeChild.Width, row * sizeChild.Height), sizeChild);

                // 자식에 대해 Arrange를 호출
                InternalChildren[index].Arrange(rectChild);
            }

            return sizeFinal;

        }
    }
}
