using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Petzold.PrintBanner
{
    internal class BannerDocumentPaginator : DocumentPaginator
    {
        string txt = "";
        Typeface face = new Typeface(""); // 글꼴의 스타일을 하나로 묶은 덩어리를 표현하는 클래스 : FontFamily, FontStyle, FontWeight, FontStretch를 포함
        Size sizePage;
        Size sizeMax = new Size(0, 0);

        // DocumentPaginator에 특화된 Public 속성
        public string Text
        {
            get { return txt; }
            set { txt = value; }
        }

        public Typeface Typeface
        {
            get { return face; }
            set { face = value; }
        }

        // FptmattedText 객체를 생성하는 Private 메서드
        FormattedText GetFormattedText(char ch, Typeface face, double em)
        {
            // em은 FontSize를 의미
            return new FormattedText(ch.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, face, em, Brushes.Black);
        }

        // PageCount가 확정됐는지 여부를 나타내는 속성
        public override bool IsPageCountValid
        {
            get
            {
                // 100em 사이즈를 기반으로 문자의 최대 크키를 결정
                foreach (char ch in txt)
                {
                    FormattedText formtxt = GetFormattedText(ch, face, 100);
                    sizeMax.Width = Math.Max(sizeMax.Width, formtxt.Width);
                    sizeMax.Height = Math.Max(sizeMax.Height, formtxt.Height);
                }

                return true;
            }
        }

        // 문서가 몇 페이지인지
        public override int PageCount
        {
            get { return txt == null ? 0 : txt.Length; }
        }

        public override Size PageSize
        {
            set { sizePage = value; }
            get { return sizePage; }
        }

        // numPage번째 페이지를 그려서 DocumentPage로 만들어서 반환하는 메서드
        public override DocumentPage GetPage(int numPage)
        {
            DrawingVisual vis = new DrawingVisual();
            DrawingContext dc = vis.RenderOpen();

            // em 사이즈의 factor를 계산할 때 1/2인치 여백을 가정
            double factor = Math.Min((PageSize.Width - 96) / sizeMax.Width, (PageSize.Height - 96) / sizeMax.Height);

            FormattedText formtxt = GetFormattedText(txt[numPage], face, factor * 100);

            // 페이지 중앙에 위치할 수 있도록 위치를 조정
            Point ptText = new Point((PageSize.Width - formtxt.Width) / 2, (PageSize.Height - formtxt.Height) / 2);

            dc.DrawText(formtxt, ptText);
            dc.Close();

            return new DocumentPage(vis);
        }

        public override IDocumentPaginatorSource Source
        {
            get { return null; }
        }
    }
}