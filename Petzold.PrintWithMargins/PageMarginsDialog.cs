using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Petzold.PrintWithMargins
{
    internal class PageMarginsDialog : Window
    {
        // 종이 테두리를 참조하는 내부 열거형
        enum Side
        {
            Left, Right, Top, Bottom,
        }

        // 숫자 입력을 위한 텍스트 박스 4개
        TextBox[] txtbox = new TextBox[4];
        Button btnOk;

        // 페이지 여백을 위한 Thickness 타입의 Public 속성
        public Thickness PageMargins
        {
            set
            {
                txtbox[(int)Side.Left].Text = (value.Left / 96).ToString("F3");
                txtbox[(int)Side.Right].Text = (value.Right / 96).ToString("F3");
                txtbox[(int)Side.Top].Text = (value.Top / 96).ToString("F3");
                txtbox[(int)Side.Bottom].Text = (value.Bottom / 96).ToString("F3");
            }
            get
            {
                return new Thickness
                    (
                        Double.Parse(txtbox[(int)Side.Left].Text),
                        Double.Parse(txtbox[(int)Side.Top].Text),
                        Double.Parse(txtbox[(int)Side.Right].Text),
                        Double.Parse(txtbox[(int)Side.Bottom].Text)
                    );
            }
        }

        public PageMarginsDialog()
        {
            // 대화상자를 위한 표준 설정
            Title = "Page Setup";
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.NoResize;

            // 윈도우 Content를 위한 스택 패널
            StackPanel stack = new StackPanel();
            Content = stack;

            // StackPanel 자식으로 GroupBox 생성
            GroupBox grpBox = new GroupBox();
            grpBox.Header = "Margins (inches)";
            grpBox.Margin = new Thickness(12);
            stack.Children.Add(grpBox);

            // GroupBox Content를 그리드로 설정
            Grid grid = new Grid();
            grid.Margin = new Thickness(6);
            grpBox.Content = grid;

            // 2개의 행과 4개의 열 생성
            for (int i = 0; i < 2; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowdef);
            }

            for (int i = 0; i < 4; i++)
            {
                ColumnDefinition coldef = new ColumnDefinition();
                coldef.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(coldef);
            }

            // 그리드에 레이블과 텍스트 박스 추가
            for (int i = 0; i < 4; i++)
            {
                txtbox[i] = new TextBox();
                txtbox[i].TextChanged += TextBoxOnTextChanged;
                txtbox[i].MinWidth = 48;
                txtbox[i].Margin = new Thickness(6);
                grid.Children.Add(txtbox[i]);
                Grid.SetRow(txtbox[i], i / 2);
                Grid.SetColumn(txtbox[i], 2 * (i % 2) + 1);
            }
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
