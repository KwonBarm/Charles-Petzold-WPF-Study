using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Petzold.PopupContextMenu
{
    internal class PopupContextMenu : Window
    {
        ContextMenu menu;
        MenuItem itemBold, itemItalic;
        MenuItem[] itemDecor;
        Inline inlClicked;
            

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PopupContextMenu());
        }
        
        public PopupContextMenu()
        {
            Title = "Popup Context Menu";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // ContextMenu 생성
            menu = new ContextMenu();

            // "Bold" 항목 추가
            itemBold = new MenuItem();
            itemBold.Header = "Bold";
            menu.Items.Add(itemBold);

            // "Italic" 항목 추가
            itemItalic = new MenuItem();
            itemItalic.Header = "Italic";
            menu.Items.Add(itemItalic);

            // 모든 TextDecorationLocation 맴버를 구함
            TextDecorationLocation[] locs = (TextDecorationLocation[])System.Enum.GetValues(typeof(TextDecorationLocation));

            // MenuItem 객체 배열을 생성해 채움
            itemDecor = new MenuItem[locs.Length];

            for (int i =0; i< locs.Length; i++)
            {
                TextDecoration decor = new TextDecoration();
                decor.Location = locs[i];

                itemDecor[i] = new MenuItem();
                itemDecor[i].Header = locs[i].ToString();
                itemDecor[i].Tag = decor;
                menu.Items.Add(itemDecor[i]);
            }

            // 전체 컨텍스트 메뉴를 핸들러 한 개로 처리
            menu.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(MenuOnClick));

            // 윈도우 Content를 위한 TextBlock 생성
            TextBlock text = new TextBlock();
            text.FontSize = 32;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            Content = text;

            // 문장을 단어로 분리
            string strQuote = "To be, or not to be, that is the question";
            string[] strWorrds = strQuote.Split();

            // 각 단어로 Run 객체를 만들어서 TextBlock에 추가
            foreach(string str in strWorrds)
            {
                Run run = new Run(str);

                // TextDecorations이 실제로 컬렉션인지를 확인
                run.TextDecorations = new TextDecorationCollection();
                text.Inlines.Add(run);
                text.Inlines.Add(" ");
            }

        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonUp(e);

            if((inlClicked = e.Source as Inline) != null)
            {
                // inLine 프로퍼티에 맞는 메뉴 항목인지 검사
                itemBold.IsChecked = (inlClicked.FontWeight == FontWeights.Bold);
                itemItalic.IsChecked = (inlClicked.FontStyle == FontStyles.Italic);

                foreach(MenuItem item in itemDecor)
                    item.IsChecked = (inlClicked.TextDecorations.Contains(item.Tag as TextDecoration));

                // 컨텍스트 메뉴를 보여줌
                menu.IsOpen = true;
                e.Handled = true;
            }
        }

        private void MenuOnClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = e.Source as MenuItem;

            item.IsChecked ^= true;

            // 체크 항목이나 체크해제 항목을 기반으로 Inline을 변경
            if (item == itemBold)
                inlClicked.FontWeight = (item.IsChecked ? FontWeights.Bold : FontWeights.Normal);
            else if (item == itemItalic)
                inlClicked.FontStyle = (item.IsChecked ? FontStyles.Italic : FontStyles.Normal);
            else
            {
                if (item.IsChecked)
                    inlClicked.TextDecorations.Add(item.Tag as TextDecoration);
                else
                    inlClicked.TextDecorations.Remove(item.Tag as TextDecoration);
            }
        }
    }
}
