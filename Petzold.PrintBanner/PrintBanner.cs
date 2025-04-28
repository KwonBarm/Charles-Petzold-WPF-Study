using System.Printing;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.PrintBanner
{
    internal class PrintBanner : Window
    {
        TextBox txtBox;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PrintBanner());
        }

        public PrintBanner()
        {
            Title = "Print Banner";
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // 윈도우 Content를 위한 StackPanel 생성
            StackPanel stack = new StackPanel();
            Content = stack;

            // TextBox 생성
            txtBox = new TextBox();
            txtBox.Width = 250;
            txtBox.Margin = new Thickness(12);
            stack.Children.Add(txtBox);

            // Button 생성
            Button btn = new Button();
            btn.Content = "_Print...";
            btn.Margin = new Thickness(12);
            btn.Click += PrintOnClick;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Children.Add(btn);

            txtBox.Focus();
        }

        private void PrintOnClick(object sender, RoutedEventArgs e)
        {
            PrintDialog dig = new PrintDialog();

            if (dig.ShowDialog().GetValueOrDefault())
            {
                // 인쇄 방햑이 수직인지 확인
                PrintTicket prntkt = dig.PrintTicket;
                prntkt.PageOrientation = PageOrientation.Portrait;
                dig.PrintTicket = prntkt;

                // BannerDocumentPaginator 생성
                BannerDocumentPaginator paginator = new BannerDocumentPaginator();

                // TextBox로 Text 프로퍼티를 설정
                paginator.Text = txtBox.Text;

                // 종이의 크기를 기반으로 PageSize 속성을 설정
                paginator.PageSize = new Size(dig.PrintableAreaWidth, dig.PrintableAreaHeight);

                // 내부동작
                // 1. 먼저 PageCount를 읽음
                // 2. IsPageCountValie - 페이지 수 믿을만 해?
                // 3. PageCount만큼 루프를 돌림
                // 4. 루프안에서 GetPage를 호출 - 그려진 페이지 결과(DocumentPage)를 출력
                dig.PrintDocument(paginator, "Banner: " + txtBox.Text);
            }
        }
    }
}
