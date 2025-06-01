using System.Globalization;
using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Petzold.NotepadClone
{
    public class PlainTextDocumentPaginator : DocumentPaginator
    {
        // public 프로퍼티와 연관된 Private 필드
        char[] charsBreaks = new char[] { ' ', '-' };
        string txt = "";
        string txtHeader = null;
        Typeface face = new Typeface("");
        double em = 11;
        Size sizePage = new Size(8.5 * 96, 11 * 96); // A4 size in pixels (8.5 inches wide, 11 inches tall at 96 DPI)
        Size sizeMax = new Size(0, 0);
        Thickness margins = new Thickness(96);
        PrintTicket prntkt = new PrintTicket();
        TextWrapping txtwrap = TextWrapping.Wrap;

        // DocumentPage 객체로 각 페이지를 저장
        List<DocumentPage> listPages;

        // Public 프로퍼티
        public string Text
        {
            set { txt = value; }
            get { return txt; }
        }
        public TextWrapping TextWrapping
        {
            set { txtwrap = value; }
            get { return txtwrap; }
        }
        public Thickness Margins
        {
            set { margins = value; }
            get { return margins; }
        }
        public Typeface Typeface
        {
            set { face = value; }
            get { return face; }
        }
        public double FontSize
        {
            set { em = value; }
            get { return em; }
        }
        public PrintTicket PrintTicket
        {
            set { prntkt = value; }
            get { return prntkt; }
        }
        public string Header
        {
            set { txtHeader = value; }
            get { return txtHeader; }
        }


        public override bool IsPageCountValid
        {
            get
            {
                if (listPages == null)
                    Format();

                return true;
            }
        }


        public override int PageCount
        {
            get
            {
                if (listPages == null)
                    return 0;

                return listPages.Count;
            }
        }

        public override Size PageSize
        {
            set { sizePage = value; }
            get { return sizePage; }
        }

        public override DocumentPage GetPage(int pageNumber)
        {
            return listPages[pageNumber];
        }

        public override IDocumentPaginatorSource Source
        {
            get { return null; }
        }

        // 인쇄할 때 텍스트 한 줄이 끝났음을 알려주는 화살표를 표시하는 내부 클래스
        class PrintLine
        {
            public string String;
            public bool Flag;

            public PrintLine(string str, bool flag)
            {
                String = str;
                Flag = flag;
            }
        }

        // 전체 문서를 페이지로 포맷팅
        private void Format()
        {
            // LineWithFlag 객체로 문서의 각 줄을 저장
            List<PrintLine> listLines = new List<PrintLine>();

            // 몇 가지 기본적인 계산에 이를 이용
            FormattedText formtxtSample = GetFormattedText("W");

            // 인쇄되는 줄의 폭
            double width = PageSize.Width - Margins.Left - Margins.Right;

            // 심각한 위험: 작업중지
            if (width < formtxtSample.Width)
                return;

            string strLine;
            Pen pn = new Pen(Brushes.Black, 2);
            StringReader reader = new StringReader(txt); // 
        }



        // FormattedText 객체를 생성하는 메서드
        private FormattedText GetFormattedText(string str)
        {
            return new FormattedText(str, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, em, Brushes.Black);
        }
    }
}
