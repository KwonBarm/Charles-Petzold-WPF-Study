using System.Printing;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.PrintWithMargins
{
    class PrintWithMargins : Window
    {
        // PrintDialog의 정보를 저장하기 위한 Private 필드
        PrintQueue printqueue;
        PrintTicket prntkt;
        Thickness marginPage = new Thickness(96);

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PrintWithMargins());
        }

        public PrintWithMargins()
        {
            Title = "Print with Margins";
            FontSize = 24;
            Width = 480;
            Height = 360;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // 윈도우 Content를 위한 StackPanel 생성
            StackPanel stack = new StackPanel();
            Content = stack;

            // 페이지 설정 버튼 생성
            Button btn = new Button();
            btn.Content = "Page Set_up...";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Margin = new Thickness(24);
            btn.Click += SetupOnClick;
            stack.Children.Add(btn);

            // 인쇄 버튼 생성
            btn = new Button();
            btn.Content = "Print...";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Margin = new Thickness(24);
            btn.Click += PrintOnClick;
            stack.Children.Add(btn);
        }

        private void SetupOnClick(object sender, RoutedEventArgs e)
        {
            PageMarginsDialog dig = new PageMarginsDialog();
            dig.Owner = this;
            dig.PageMargins = marginPage;

            if (dig.ShowDialog().GetValueOrDefault())
            {
                // 대화상자의 페이지 여백을 저장
                marginPage = dig.PageMargins;
            }
        }

        private void PrintOnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
