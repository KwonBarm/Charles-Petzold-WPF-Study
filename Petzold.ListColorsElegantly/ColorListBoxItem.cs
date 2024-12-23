using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.ListColorsElegantly
{
    internal class ColorListBoxItem : ListBoxItem
    {
        string str;
        Rectangle rect;
        TextBlock text;

        public ColorListBoxItem()
        {
            // Rectangle과 TextBlock을 위한 스택패널을 생성
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;

            // 색상을 보여줄 Rectangle을 생성
            rect = new Rectangle();
            rect.Width = 16;
            rect.Height = 16;
            rect.Margin = new Thickness(2);
            rect.Stroke = SystemColors.WindowTextBrush;
            stack.Children.Add(rect);

            // 색상명을 보여줄 TextBlock 생성
            text = new TextBlock();
            text.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(text);
        }

        // Text 속성은 TextBlock의 Text 속성이 됨
        public string Text
        {
            get { return str; }
            set
            {
                str = value;
                string strSpaced = str[0].ToString();

                for (int i = 1; i < str.Length; i++)
                    strSpaced += (char.IsUpper(str[i]) ? " " : "") + str[i].ToString();
                text.Text = strSpaced;
            }
        }

        // Color 속성은 Rectangle의 Brush 속성이 됨
        public Color Color
        {
            get
            {
                SolidColorBrush brush = rect.Fill as SolidColorBrush;
                return brush == null ? Colors.Transparent : brush.Color;
            }
            set { rect.Fill = new SolidColorBrush(value); }
        }

        // 선택된 항목의 폰트를 굵게 함
        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            text.FontWeight = FontWeights.Bold;
        }

        protected override void OnUnselected(RoutedEventArgs e)
        {
            base.OnUnselected(e);
            text.FontWeight = FontWeights.Regular;
        }

        // 키보드 인터페이스를 위한 ToString 구현
        public override string ToString()
        {
            return str;
        }
    }



}
