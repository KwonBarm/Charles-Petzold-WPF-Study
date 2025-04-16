using System.Globalization;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            PrintDialog dig = new PrintDialog();

            // PrintQueue와 PrintTicket 설정
            if (printqueue != null)
                dig.PrintQueue = printqueue;

            if (prntkt != null)
                dig.PrintTicket = prntkt;

            if (dig.ShowDialog().GetValueOrDefault())
            {
                // PrintQueue와 PrintTicket을 대화상자의 값으로 설정
                printqueue = dig.PrintQueue;
                prntkt = dig.PrintTicket;

                // DrawingVisual을 생성하고 DrawingContext를 염
                DrawingVisual vis = new DrawingVisual();
                DrawingContext dc = vis.RenderOpen();
                Pen pn = new Pen(Brushes.Black, 1.0);

                // Rectangle은 여백을 뺀 페이지를 나타냄
                Rect rectPage = new Rect(marginPage.Left, marginPage.Top, dig.PrintableAreaWidth - (marginPage.Left + marginPage.Right), dig.PrintableAreaHeight - (marginPage.Top + marginPage.Bottom));

                // 사용자 여백을 반영한 사각형 출력
                dc.DrawRectangle(null, pn, rectPage);

                // PrintableArea 속성을 보여주는 포맷팅된 텍스트 객체를 생석
                FormattedText formtxt = new FormattedText
                    (
                        String.Format("Hello, Printer! {0} x {1}", dig.PrintableAreaWidth / 96, dig.PrintableAreaHeight / 96),
                        CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                        new Typeface(new FontFamily("Times New Roman"), FontStyles.Italic, FontWeights.Normal, FontStretches.Normal), 48, Brushes.Black
                    );

                // 포맷된 텍스트의 물리적 크키를 계산
                Size sizeText = new Size(formtxt.Width, formtxt.Height);

                // 여백 내의 텍스트의 중앙점을 계산 
                Point ptText = new Point(rectPage.Left + (rectPage.Width - formtxt.Width) / .2, rectPage.Top + (rectPage.Height - sizeText.Height) / 2);

                // 텍스트와 이를 둘러싸는 사각형 출력
                dc.DrawText(formtxt, ptText);
                dc.DrawRectangle(null, pn, new Rect(ptText, sizeText));

                dc.Close();

                // 인쇄 기능을 실행하기 위해서 반드시 호출 : 시각적 요소(Visual)을 프린트로 인쇄하는 역할
                // 2번째 인자는 인쇄 작업 설명 (일반적으로 인쇄 대기열에 표시됨)
                dig.PrintVisual(vis, Title);
            }
        }

    }
}
