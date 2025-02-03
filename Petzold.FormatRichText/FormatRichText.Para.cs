using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.FormatRichText
{
    internal partial class FormatRichText : Window
    {
        ToggleButton[] btnAlignment = new ToggleButton[4];

        void AddParaToolbar(ToolBarTray tray, int band, int index)
        {
            // 트레이에 추가할 툴바 생성
            ToolBar toolBar = new ToolBar();
            toolBar.Band = band;
            toolBar.BandIndex = index;
            tray.ToolBars.Add(toolBar);

            // 툴바 항목 생성
            toolBar.Items.Add(btnAlignment[0] = CreateButton(TextAlignment.Left, "Align Left", 0, 4));
            toolBar.Items.Add(btnAlignment[1] = CreateButton(TextAlignment.Center, "Center", 2, 2));
            toolBar.Items.Add(btnAlignment[2] = CreateButton(TextAlignment.Right, "Align Right", 4, 0));
            toolBar.Items.Add(btnAlignment[3] = CreateButton(TextAlignment.Justify, "Justify", 0, 0));

            // SeclectionChanged 이벤트에 대한 또다른 이벤트 핸들러를 연결
            txtbox.SelectionChanged += TextBoxOnSelectionChanged2;
        }

        
        private ToggleButton CreateButton(TextAlignment align, string strToolTip, int offsetLeft, int offsetRight)
        {
            // 토클 버튼 생성
            ToggleButton btn = new ToggleButton();
            btn.Tag = align;
            btn.Click += ButtonOnClick;

            // Canvas를 Content로 설정
            Canvas canv = new Canvas();
            canv.Width = 16;
            canv.Height = 16;
            btn.Content = canv;

            // Canvas에 선을 그림
            for (int i = 0; i < 5; i++)
            {
                Polyline poly = new Polyline();
                poly.Stroke = SystemColors.WindowTextBrush;
                poly.StrokeThickness = 1;

                if ((i & 1) == 0)
                {
                    poly.Points = new PointCollection(new Point[]
                    {
                        new Point (2, 2+3*i), new Point(14, 2+3*i)
                    });
                }
                else
                {
                    poly.Points = new PointCollection(new Point[]
                    {
                        new Point(2+ offsetLeft, 2+3*i), new Point(14-offsetRight, 2+3*i)
                    });
                }

                canv.Children.Add(poly);

            }

            // 툴팁 생성
            ToolTip tip = new ToolTip();
            tip.Content = strToolTip;
            btn.ToolTip = tip;

            return btn;
        }

        private void TextBoxOnSelectionChanged2(object sender, RoutedEventArgs e)
        {
            // 현재 텍스트 정렬 상태를 구함
            object obj = txtbox.Selection.GetPropertyValue(Paragraph.TextAlignmentProperty);

            // 버튼 설정
            if (obj != null && obj is TextAlignment)
            {
                TextAlignment align = (TextAlignment)obj;

                foreach (ToggleButton btn in btnAlignment)
                    btn.IsChecked = (align == (TextAlignment)btn.Tag);
            }
            else
            {
                foreach(ToggleButton btn in btnAlignment)
                    btn.IsChecked = false;
            }
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = e.Source as ToggleButton;

            foreach (ToggleButton btnAlign in btnAlignment)
                btnAlign.IsChecked = (btn == btnAlign);

            // 새로운 텍스트 정렬 설정
            TextAlignment align = (TextAlignment)btn.Tag;
            txtbox.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, align);
        }
    }
}
