using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.PrintaBunchaButtons
{
    internal class PrintaBunchaButtons : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PrintaBunchaButtons());
        }

        public PrintaBunchaButtons()
        {
            Title = "Print a Bunch of Buttons";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = 400;
            Height = 300;

            // 'Print' 버튼 생성
            Button btn = new Button();
            btn.FontSize = 24;
            btn.Content = "Print...";
            btn.Padding = new Thickness(12);
            btn.Margin = new Thickness(96);
            btn.Click += PrintOnClick;
            Content = btn;
        }

        private void PrintOnClick(object sender, RoutedEventArgs e)
        {
            PrintDialog dig = new PrintDialog();

            if (dig.ShowDialog().GetValueOrDefault())
            {
                // 그리드 패널 생성
                Grid grid = new Grid();

                // 자동으로 크기가 변하는 열과 행을 5개 정의
                for (int i = 0; i < 5; i++)
                {
                    ColumnDefinition coldef = new ColumnDefinition();
                    coldef.Width = GridLength.Auto;
                    grid.ColumnDefinitions.Add(coldef);

                    RowDefinition rowdef = new RowDefinition();
                    rowdef.Height = GridLength.Auto;
                    grid.RowDefinitions.Add(rowdef);
                }

                // 그라디언트 브러시로 그리드의 배경색을 지정
                grid.Background = new LinearGradientBrush(Colors.Gray, Colors.White, new Point(0, 0), new Point(1, 1));

                // 난수 생성
                Random rand = new Random();

                // 25개 버튼으로 Grid를 채움
                for (int i = 0; i < 25; i++)
                {
                    Button btn = new Button();
                    btn.FontSize = 12 + rand.Next(8);
                    btn.Content = "Button No. " + (i + 1);
                    btn.HorizontalAlignment = HorizontalAlignment.Center;
                    btn.VerticalAlignment = VerticalAlignment.Center;
                    btn.Margin = new Thickness(6);
                    grid.Children.Add(btn);
                    Grid.SetRow(btn, i / 5);
                    Grid.SetColumn(btn, i % 5);
                }

                // 그리드 크기 결정 
                grid.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

                Size sizeGrid = grid.DesiredSize;

                // 페이지상의 그리드의 중앙점을 결정
                Point ptGrid = new Point((dig.PrintableAreaWidth - sizeGrid.Width) / 2, (dig.PrintableAreaHeight - sizeGrid.Height) / 2);

                // 레이아웃은 설정하지 않고 통과
                grid.Arrange(new Rect(ptGrid, sizeGrid));
                grid.UpdateLayout();

                //Test Window
                Window previewWindow = new Window();
                previewWindow.Content = grid;
                previewWindow.Show();
                previewWindow.SizeToContent = SizeToContent.WidthAndHeight;

                // 인쇄
                dig.PrintVisual(grid, Title);
            }
        }
    }
}
