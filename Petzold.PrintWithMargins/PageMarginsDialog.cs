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
            Left, Top, Right, Bottom
        }

        // 숫자 입력을 위한 TextBox 4 개
        TextBox[] txtbox = new TextBox[4];
        Button btnOk;

        // 페이지 여백을 위한 Thickness 타입의 Public 속성
        public Thickness PageMargins
        {
            set
            {
                txtbox[(int)Side.Left].Text = (value.Left / 96).ToString("F3");
                txtbox[(int)Side.Right].Text = (value.Right /96).ToString("F3");
                txtbox[(int)Side.Top].Text = (value.Top / 96).ToString("F3");
                txtbox[(int)Side.Bottom].Text = (value.Bottom / 96).ToString("F3");
            }
            get
            {
                return new Thickness
                    (
                        double.Parse(txtbox[(int)Side.Left].Text) * 96,
                        double.Parse(txtbox[(int)Side.Top].Text) * 96,
                        double.Parse(txtbox[(int)Side.Right].Text) * 96,
                        double.Parse(txtbox[(int)Side.Bottom].Text) * 96
                    );
            }
        }

        public PageMarginsDialog()
        {
            // 대화상자를 위한 표준 설정
            Title = "Page Setup";
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.NoResize;

            // 윈도우 Content를 위한 StackPanel
            StackPanel stack = new StackPanel();
            Content = stack;

            // StackPanel의 자식으로 그룹 박스를 생성
            GroupBox grpbox = new GroupBox();
            grpbox.Header = "Margins (inches)";
            grpbox.Margin = new Thickness(12);
            stack.Children.Add(grpbox);

            // GroupBox의 Content로 Grid를 생성
            Grid grid = new Grid();
            grid.Margin = new Thickness(6);
            grpbox.Content = grid;

            // 2개의 행과 4개의 열
            for(int i = 0; i < 2; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowdef);
            }

            for(int i=0; i<4; i++)
            {
                ColumnDefinition colder = new ColumnDefinition();
                colder.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(colder);
            }

            // Grid에 Label과 TextBox를 추가
            for(int i = 0; i < 4 ; i++)
            {
                Label lbl = new Label();
                lbl.Content = "_" + Enum.GetName(typeof(Side), i) + ":";
                lbl.Margin = new Thickness(6);
                lbl.VerticalAlignment = VerticalAlignment.Center;
                grid.Children.Add(lbl);
                Grid.SetRow(lbl, i / 2);
                Grid.SetColumn(lbl, 2 * (i % 2));

                txtbox[i] = new TextBox();
                txtbox[i].TextChanged += TextBoxOnTextChanged;
                txtbox[i].MinWidth = 48;
                txtbox[i].Margin = new Thickness(6);
                grid.Children.Add(txtbox[i]);
                Grid.SetRow(txtbox[i], i / 2);
                Grid.SetColumn(txtbox[i], 2 * (i % 2) + 1);
            }

            // OK와 Cancle 버튼을 추가하기 위해 UniformGrid를 생성
            UniformGrid unigrid = new UniformGrid();
            unigrid.Rows = 1;
            unigrid.Columns = 2;
            stack.Children.Add(unigrid);

            btnOk = new Button();
            btnOk.Content = "OK";
            btnOk.IsDefault = true;
            btnOk.IsEnabled = false;
            btnOk.MinWidth = 60;
            btnOk.Margin = new Thickness(12);
            btnOk.HorizontalAlignment = HorizontalAlignment.Center;
            btnOk.Click += OkButtonOnClick;
            unigrid.Children.Add(btnOk);

            Button btnCancel = new Button();
            btnCancel.Content = "Cancel";
            btnCancel.IsCancel = true;
            btnCancel.MinWidth = 60;
            btnCancel.Margin = new Thickness(12);
            btnCancel.HorizontalAlignment = HorizontalAlignment.Center;
            unigrid.Children.Add(btnCancel);

            // TextBox의 값이 숫자이면 OK 버튼을 활성화
        }

        // OK 버튼을 클릭하면 대화상자를 종료
        private void OkButtonOnClick(object sender, RoutedEventArgs e)
        {
            // WPF에서 모달 대화 상자(Modal Dialog)의 결과를 나타내는 속성
            // ShowDialog()로 열린 창에서만 사용 가능
            DialogResult = true;
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            double result;

            btnOk.IsEnabled =
                Double.TryParse(txtbox[(int)Side.Left].Text, out result) &&
                Double.TryParse(txtbox[(int)Side.Top].Text, out result) &&
                Double.TryParse(txtbox[(int)Side.Right].Text, out result) &&
                Double.TryParse(txtbox[(int)Side.Bottom].Text, out result);
        }

    }
}
