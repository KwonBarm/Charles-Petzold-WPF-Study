using Petzold.PrintWithMargins;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Petzold.NotepadClone
{
    public partial class NotepadClone : Window
    {
        // 인쇄를 위한 필드
        PrintQueue prnqueue;
        PrintTicket prntkt;
        Thickness marginPage = new Thickness(96);

        void AddPrintMenuItems(MenuItem itemFile)
        {
            // Page Setup 메뉴 항목
            MenuItem itemSetup = new MenuItem();
            itemSetup.Header = "Page Set_up...";
            itemSetup.Click += PageSetupOnClick;
            itemFile.Items.Add(itemSetup);

            // Print 메뉴 항목
            MenuItem itemPrint = new MenuItem();
            itemPrint.Header = "_Print...";
            itemPrint.Command = ApplicationCommands.Print;
            itemFile.Items.Add(itemPrint);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Print, PrintOnExecuted));
        }

        private void PageSetupOnClick(object sender, RoutedEventArgs e)
        {
            // 대화상자를 만들고 PageMargins 속성을 초기화
            PageMarginsDialog dig = new PageMarginsDialog();
            dig.Owner = this;
            dig.PageMargins = marginPage;

            if (dig.ShowDialog().GetValueOrDefault())
            {
                // 대화상자로 페이지 여백을 저장
                marginPage = dig.PageMargins;
            }
        }

        private void PrintOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PrintDialog dig = new PrintDialog();

            // 이전 실행에서의 PrintQueue와 PrintTicket을 구함
            if(prnqueue != null)
                dig.PrintQueue = prnqueue;

            if(prntkt != null)
                dig.PrintTicket = prntkt;

            if (dig.ShowDialog().GetValueOrDefault())
            {
                // 대화상자에서 PrintQueue와 PrintTicket을 저장
                prnqueue = dig.PrintQueue;
                prntkt = dig.PrintTicket;

                // PlainTextDocumentPaginator 객체 생성
            }
        }

        
    }
}
