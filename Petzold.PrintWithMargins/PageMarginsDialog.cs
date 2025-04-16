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
                        Double.Parse(txtbox[(int)Side.Left].Text) * 96,
                        Double.Parse(txtbox[(int)Side.Top].Text) * 96,
                        Double.Parse(txtbox[(int)Side.Right].Text) * 96,
                        Double.Parse(txtbox[(int)Side.Bottom].Text) * 96
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
                Label lbl = new Label();
                lbl.Content = "_" + Enum.GetName(typeof(Side), i) + ":";
                lbl.Margin = new Thickness(6);
                lbl.VerticalAlignment = VerticalAlignment.Center;
                grid.Children.Add(lbl);
                Grid.SetRow(lbl, i / 2);
                Grid.SetColumn(lbl, 2 * (i % 2));

                txtbox[i] = new TextBox();
                txtbox[i].VerticalContentAlignment = VerticalAlignment.Center;
                txtbox[i].TextChanged += TextBoxOnTextChanged;
                txtbox[i].MinWidth = 48;
                txtbox[i].Margin = new Thickness(6);
                grid.Children.Add(txtbox[i]);
                Grid.SetRow(txtbox[i], i / 2);
                Grid.SetColumn(txtbox[i], 2 * (i % 2) + 1);
            }

            // OK와 Cancel 버튼을 위해 UniformGrid 생성
            UniformGrid uniformGrid = new UniformGrid();
            uniformGrid.Rows = 1;
            uniformGrid.Columns = 2;
            stack.Children.Add(uniformGrid);

            btnOk = new Button();
            btnOk.Content = "OK";
            btnOk.IsDefault = true;
            btnOk.IsEnabled = false;
            btnOk.MinWidth = 60;
            btnOk.Margin = new Thickness(12);
            btnOk.HorizontalAlignment = HorizontalAlignment.Center;
            btnOk.Click += OKButtonOnClick;
            uniformGrid.Children.Add(btnOk);

            Button btnCancle = new Button();
            btnCancle.Content = "Cancel";
            btnCancle.IsCancel = true; // ShowDialog()로 해당 윈도우를 열면 IsCancel가 true일때 Cancle 버튼을 클릭하면 닫힘
            btnCancle.MinWidth = 60;
            btnCancle.Margin = new Thickness(12);
            btnCancle.HorizontalAlignment = HorizontalAlignment.Center;
            uniformGrid.Children.Add(btnCancle);
        }

        // 텍스트 박스의 값이 숫자이면 OK 버튼을 활성화
        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            double result;

            btnOk.IsEnabled =
                Double.TryParse(txtbox[(int)Side.Left].Text, out result) &&
                Double.TryParse(txtbox[(int)Side.Right].Text, out result) &&
                Double.TryParse(txtbox[(int)Side.Top].Text, out result) &&
                Double.TryParse(txtbox[(int)Side.Bottom].Text, out result);
        }

        // OK를 클릭하면 대화상자를 종료함 
        private void OKButtonOnClick(object sender, RoutedEventArgs e)
        {
            // 모달 창(ShowDialog)에서만 사용 가능
            // 대화상자를 닫으면서 DialogResult를 true로 설정
            DialogResult = true;
        }


    }
}
