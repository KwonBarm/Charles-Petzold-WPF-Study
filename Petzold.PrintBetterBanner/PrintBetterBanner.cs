using System.Globalization;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Petzold.ChooseFont;
using Petzold.PrintBanner;

namespace Petzold.PrintBetterBanner
{
    internal class PrintBetterBanner : Window
    {
        TextBox textBox;
        Typeface face;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PrintBetterBanner());
        }

        public PrintBetterBanner()
        {
            Title = "Print Better Banner";
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Window Content를 위한 StackPanel 생성
            StackPanel stack = new StackPanel();
            Content = stack;

            // TextBox 생성
            textBox = new TextBox();
            textBox.Width = 250;
            textBox.Margin = new Thickness(12);
            stack.Children.Add(textBox);

            // Font Button 생성
            Button btn = new Button();
            btn.Content = "_Font...";
            btn.Margin = new Thickness(12);
            btn.Click += FontOnClick;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Children.Add(btn);

            // 인쇄 버튼 생성
            btn = new Button();
            btn.Content = "_Print...";
            btn.Margin = new Thickness(12);
            btn.Click += PrintOnClick;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Children.Add(btn);

            // Facename 필드 초기화
            face = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);

            textBox.Focus();
        }

       

        private void FontOnClick(object sender, RoutedEventArgs e)
        {
            FontDialog dig = new FontDialog();
            dig.Owner = this;
            dig.Typeface = face;

            if (dig.ShowDialog().GetValueOrDefault())
            {
                face = dig.Typeface;
            }

        }

        private void PrintOnClick(object sender, RoutedEventArgs e)
        {
            PrintDialog dig = new PrintDialog();

            if (dig.ShowDialog().GetValueOrDefault())
            {
                // 인쇄 방향이 수직인지 확인
                PrintTicket prntkt = dig.PrintTicket;
                prntkt.PageOrientation = PageOrientation.Portrait; // 인쇄 방향을 세로로 설정
                dig.PrintTicket = prntkt;

                // DocumentPaginator 객체 생성
                BannerDocumentPaginator paginator = new BannerDocumentPaginator();

                // TextBox 속성 설정
                paginator.Text = textBox.Text;

                // 용지 크기를 기반으로 PageSize 설정
                paginator.PageSize = new Size(dig.PrintableAreaWidth, dig.PrintableAreaHeight);

                // 문서를 인쇄하기 위해 PrintDocument 호출
                dig.PrintDocument(paginator, "Banner: " + textBox.Text);
            }
        }
    }
}
