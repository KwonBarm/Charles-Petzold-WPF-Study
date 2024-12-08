using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.SelectColor
{
    internal class ColorGrid : Control
    {
        // 행과 열의 수
        const int yNum = 5;
        const int xNum = 8;

        // 출력할 색상들
        string[,] strColors = new string[yNum, xNum]
        {
            { "Black", "Brown", "DarkGreen", "MidnightBlue", "Navy", "DarkRed", "DarkMagenta", "Olive" },
            { "DarkGray", "Gray", "Green", "Blue", "Indigo", "Red", "Magenta", "Yellow" },
            { "White", "LightGray", "Lime", "Cyan", "SkyBlue", "Orange", "Pink", "Transparent" },
            { "AliceBlue", "AntiqueWhite", "Aqua", "Aquamarine", "Azure", "Beige", "Bisque", "BlanchedAlmond" },
            { "BlueViolet", "BurlyWood", "CadetBlue", "Chartreuse", "Chocolate", "Coral", "CornflowerBlue", "Cornsilk" }
        };

        // ColorCell 객체 생성
        ColorCell[,] cells = new ColorCell[yNum, xNum];
        ColorCell cellSelected;
        ColorCell cellHighlighted;

        // 이 컨트롤을 구성할 엘리먼트
        Border bord;
        UniformGrid unigrid;

        // 현재 선택된 색상
        Color clrSelected = Colors.Black;

        // public "Changed" 이벤트
        public event EventHandler SelectedColorChanged;

        // Public 생성자
        public ColorGrid()
        {
            // 컨트롤의 Border 생성
            bord = new Border();
            bord.BorderBrush = SystemColors.ControlDarkDarkBrush;
            bord.BorderThickness = new Thickness(1);
            AddVisualChild(bord);
            AddLogicalChild(bord);

            // UniformGrid을 생성해 Border의 Child로 설정
            unigrid = new UniformGrid();
            unigrid.Background = SystemColors.WindowBrush;
            unigrid.Columns = xNum;
            bord.Child = unigrid;

            // UniformGrid에 ColorCell 객체들을 채움
            for (int y = 0; y < yNum; y++)
                for (int x = 0; x < xNum; x++)
                {
                    Color clr = (Color)typeof(Colors).GetProperty(strColors[y, x]).GetValue(null, null);

                    cells[y, x] = new ColorCell(clr);
                    unigrid.Children.Add(cells[y, x]);

                    if (clr == SelectedColor)
                    {
                        cellSelected = cells[y, x];
                        cells[y, x].IsSelected = true;
                    }


                }
        }

        // Public SelectedColor 프로퍼티, get만 지원
        public Color SelectedColor
        {
            get
            {
                return clrSelected;
            }

        }

        // VisualChildrenCount 프로퍼티 오버라이딩
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        // GetVisualChild 메소드 오버라이딩
        protected override Visual GetVisualChild(int index)
        {
            if (index > 0)
                throw new ArgumentOutOfRangeException("index");

            return bord;
        }

        // MeasureOverride 메소드 오버라이딩
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            bord.Measure(sizeAvailable);
            return bord.DesiredSize;
        }

        // ArrangeOverride 메소드 오버라이딩
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            bord.Arrange(new Rect(new Point(0, 0), sizeFinal));

            return sizeFinal;
        }

        // 마우스 이벤트 처리
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            if (cellHighlighted != null)
            {
                cellHighlighted.IsHighlighted = false;
                cellHighlighted = null;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            ColorCell cell = e.Source as ColorCell;

            if (cell != null)
            {
                if (cellHighlighted != null)
                    cellHighlighted.IsHighlighted = false;

                cellHighlighted = cell;
                cellHighlighted.IsHighlighted = true;
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            if (cellHighlighted != null)
            {
                cellHighlighted.IsHighlighted = false;
                cellHighlighted = null;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            ColorCell cell = e.Source as ColorCell;

            if (cell != null)
            {
                if (cellHighlighted != null)
                    cellHighlighted.IsHighlighted = false;

                cellHighlighted = cell;
                cellHighlighted.IsHighlighted = true;
            }
            Focus();
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            ColorCell cell = e.Source as ColorCell;

            if (cell != null)
            {
                if (cellSelected != null)
                    cellSelected.IsSelected = false;

                cellSelected = cell;
                cellSelected.IsSelected = true;

                clrSelected = (cellSelected.Brush as SolidColorBrush).Color;
                OnSelectedColorChanged(EventArgs.Empty);
            }
        }

        // 키보드 이벤트 처리
        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);

            if (cellSelected != null)
                cellHighlighted = cellSelected;
            else
                cellHighlighted = cells[0, 0];

            cellHighlighted.IsHighlighted = true;
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);

            if (cellHighlighted != null)
            {
                cellHighlighted.IsHighlighted = false;
                cellHighlighted = null;
            }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            int index = unigrid.Children.IndexOf(cellHighlighted);
            int y = index / xNum;
            int x = index % xNum;

            switch (e.Key)
            {
                case Key.Home:
                    y = 0;
                    x = 0;
                    break;
                case Key.End:
                    y = yNum - 1;
                    x = xNum - 1;
                    break;
                case Key.Down:
                    if ((y = (y + 1) % yNum) == 0)
                        x++;
                    break;
                case Key.Up:
                    if ((y = (y + yNum - 1) % yNum) == yNum - 1)
                        x--;
                    break;
                case Key.Left:
                    if ((x = (x + xNum - 1) % xNum) == xNum - 1)
                        y--;
                    break;
                case Key.Right:
                    if ((x = (x + 1) % xNum) == 0)
                        y++;
                    break;
                case Key.Enter:
                case Key.Space:
                    if (cellHighlighted != null)
                        cellHighlighted.IsSelected = false;

                    cellSelected = cellHighlighted;
                    cellSelected.IsSelected = true;
                    clrSelected = (cellSelected.Brush as SolidColorBrush).Color;
                    OnSelectedColorChanged(EventArgs.Empty);
                    break;

                default:
                    return;
            }

            if(x >= xNum || y>= yNum) 
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            else if (x < 0 || y < 0)
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
            else
            {
                cellHighlighted.IsHighlighted = false;
                cellHighlighted = cells[y, x];
                cellHighlighted.IsHighlighted = true;
            }

            e.Handled = true;
        }

        // SelectedColorChanged 이벤트 발생 시 Protected 메소드
        protected virtual void OnSelectedColorChanged(EventArgs args)
        {
            if (SelectedColorChanged != null)
                SelectedColorChanged(this, args);
        }
    }
}