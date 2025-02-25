using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.PrintEllipse
{
    internal class PrintEllipse : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            new PrintEllipse().Show();
            app.Run();
        }

        public PrintEllipse()
        {
            Title = "Print Ellipse";
            FontSize = 24;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = 500;
            Height = 300;

            // 윈도우 Content를 위한 StackPanel 생성
            StackPanel stack = new StackPanel();
            Content = stack;

            // 인쇄를 위한 버튼 생성
            Button btn = new Button();
            btn.Content = "_Print...";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Margin = new Thickness(24);
            btn.Click += PrintOnClick;
            stack.Children.Add(btn);
        }

        private void PrintOnClick(object sender, RoutedEventArgs e)
        {
            PrintDialog dig = new PrintDialog();

            // GetValueOrDefault() 메서드는 Nullable 값 타입(bool? 같은 타입)의 값이 null인지 확인하고  , null이면 기본값을 반환
            // 사용자가 Dialog에서 인쇄 버튼을 누르면 true를 반환하고, 취소 버튼을 누르면 false를 반환
            // 사용자가 제목 옆에 닫기 버튼을 누르면 null을 반환하고 GetValueOrDefault() 메서드는 false를 반환
            if (dig.ShowDialog().GetValueOrDefault())
            {
                // DrawingVisual을 생성하고 DrawingContext를 준비
                DrawingVisual vis = new DrawingVisual(); // 빈 캔버스를 준비하는 단계
                DrawingContext dc = vis.RenderOpen(); // 그릴 준비를 마치는 과정

                // 타원을 출력
                dc.DrawEllipse(Brushes.LightGray, new Pen(Brushes.Black, 3), new Point(dig.PrintableAreaWidth / 2 , dig.PrintableAreaHeight /2), dig.PrintableAreaWidth / 2 , dig.PrintableAreaHeight / 2);

                // DrawingContext를 닫음
                dc.Close();

                // 끝으로 페이지를 인쇄
                dig.PrintVisual(vis, "My first print job");
            }
        }
    }
}
