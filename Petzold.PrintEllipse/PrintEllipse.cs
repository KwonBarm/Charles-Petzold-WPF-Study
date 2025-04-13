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
            app.Run(new PrintEllipse());
        }

        public PrintEllipse()
        {
            Title = "Print Ellipse";
            FontSize = 24;

            // 윈도우 Content를 위한 StackPanel을 생성
            StackPanel stack = new StackPanel();
            Width = 300;
            Height = 200;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Content = stack;

            // 인새를 위한 버튼 생성
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

            // ShowDialog() 메서드는 사용자가 인쇄 버튼을 누르면 true를 반환하고, 취소 버튼을 누르면 false를 반환
            // GetValueOrDefault() 메서드는 Nullable 값 타입(bool? 같은 타입)의 값이 null인지 확인하고  , null이면 기본값을 반환
            // 사용자가 제목 옆에 닫기 버튼을 누르면 null을 반환하고 GetValueOrDefault() 메서드는 false를 반환한다고 했지만 실제 테스트 결과 false가 반환됨
            if (dig.ShowDialog().GetValueOrDefault())
            {
                // DrawingVisual을 생성하고 DrawingContext를 준비
                DrawingVisual vis = new DrawingVisual();
                DrawingContext dc = vis.RenderOpen();

                // 타원 그리기
                dc.DrawEllipse(Brushes.LightGray, new Pen(Brushes.Black,3),new Point(dig.PrintableAreaWidth/2, dig.PrintableAreaHeight/2),dig.PrintableAreaWidth/2,dig.PrintableAreaHeight/2);

                // DrawingContext을 닫음
                dc.Close();

                // 페이지를 인쇄
                dig.PrintVisual(vis, "My first Print job");
            }
        }


        
    }
}
