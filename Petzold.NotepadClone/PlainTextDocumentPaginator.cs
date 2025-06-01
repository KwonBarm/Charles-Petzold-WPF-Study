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
            StringReader reader = new StringReader(txt);

            // listLines에 각 줄을 저장하기 위해 ProcessLine을 호출
            while (null != (strLine = reader.ReadLine()))
                ProcessLine(strLine, width, listLines);
        }

        // str - txt에서 읽어온 한 줄
        // width - 페이지 폭
        // list - PrintLine 객체를 저장할 리스트
        private void ProcessLine(string str, double width, List<PrintLine> list)
        {
            str = str.TrimEnd(' '); // 문자열의 끝에 있는 공백 제거

            // TextWrapping == TextWrapping.NoWrap인 경우
            // ------------------------------------------
            if(TextWrapping == TextWrapping.NoWrap)
            {
                do
                {
                    int length = str.Length;

                    // 문자열의 폭이 width(페이지 폭)보다 작거나 같을 때까지 문자열의 길이값을 줄임
                    while (GetFormattedText(str.Substring(0, length)).Width > width)
                        length--;

                    // width(페이지 폭)만큼 문자열을 잘라서 PrintLine 객체에 저장
                    // 그리고 나머지 문자열을 str에 저장
                    list.Add(new PrintLine(str.Substring(0, length), length < str.Length));
                    str = str.Substring(length); // str에서 시작부터 length 길이만큼 잘라냄
                }
                while (str.Length > 0);
            }

            // TextWrapping == TextWrapping.Wrap인 경우 또는
            // TextWrapping == TextWrapping.WrapWithOverflow인 경우
            // ---------------------------------------------------
            else
            {
                do
                {
                    int length = str.Length;
                    bool flag = false;

                    while (GetFormattedText(str.Substring(0, length)).Width > width)
                    {
                        // LastIndexOfAny는 인수 charsBreaks 문자배열 중 가장 마지막에 나타나는 문자의 인덱스를 반환
                        int index = str.LastIndexOfAny(charsBreaks, length - 2);

                        // index가 -1이 아니면 공백이나 대시가 있는 위치를 찾았다는 의미
                        if (index != -1)
                            length = index + 1; // 공백이나 대시를 포함
                        else
                        {
                            // 특정 여역 내에서 공백이나
                            // 대시가 들어갈 수도 있다는 점을 기억할 것
                            // 공백이나 대시가 있는지를 검사
                            index = str.IndexOfAny(charsBreaks);

                            if (index != -1)
                                length = index + 1;

                            // TextWrapping == TextWrapping.WrapWithOverflow이면 단순히 줄을 출력
                            // TextWrapping == TextWrapping.Wrap이면 플래그를 설정하고 루프 탈출
                            if (TextWrapping == TextWrapping.Wrap)
                            {
                                while(GetFormattedText(str.Substring(0, length)).Width > width)
                                    length--;

                                flag = true; // 줄이 잘렸음을 알림
                            }
                            break; // 루프 탈출
                        }
                    }

                    list.Add(new PrintLine(str.Substring(0, length), flag));
                    str = str.Substring(length); // str에서 시작부터 length 길이만큼 잘라냄
                }
                while (str.Length > 0);
            }
        }

        // FormattedText 객체를 생성하는 메서드
        private FormattedText GetFormattedText(string str)
        {
            return new FormattedText(str, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, em, Brushes.Black);
        }
    }
}
