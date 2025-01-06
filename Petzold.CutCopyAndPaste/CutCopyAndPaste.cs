using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Petzold.CutCopyAndPaste
{
    class CutCopyAndPaste : Window
    {
        TextBlock text;
        protected MenuItem itemCut, itemCopy, itemPaste, itemDelete;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CutCopyAndPaste());
        }

        public CutCopyAndPaste()
        {
            Title = "Cut, Copy, and Paste";

            // DockPanel 생성
            DockPanel dock = new DockPanel();
            Content = dock;

            // 탑 메뉴가 될 Menu 생성
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            // 나머지를 채울 TextBlock 생성
            text = new TextBlock();
            text.Text = "Sample clipboard text";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.FontSize = 32;
            text.TextWrapping = TextWrapping.Wrap;
            dock.Children.Add(text);

            // Edit 메뉴 생성
            MenuItem itemEdit = new MenuItem();
            itemEdit.Header = "_Edit";
            itemEdit.SubmenuOpened += EditOnOpened;
            menu.Items.Add(itemEdit);

            // Edit 메뉴 항목 생성
            itemCut = new MenuItem();
            itemCut.Header = "Cu_t";
            itemCut.Click += CutOnClick;
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,/Images/Cut.png"));
            itemCut.Icon = img;
            itemEdit.Items.Add(itemCut);

            itemCopy = new MenuItem();
            itemCopy.Header = "_Copy";
            itemCopy.Click += CopyOnClick;
            img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,/Images/Cut.png"));
            itemCopy.Icon = img;
            itemEdit.Items.Add(itemCopy);

            itemPaste = new MenuItem();
            itemPaste.Header = "_Paste";
            itemPaste.Click += PasteOnClick;
            img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,/Images/Cut.png"));
            itemPaste.Icon = img;
            itemEdit.Items.Add(itemPaste);

            itemDelete = new MenuItem();
            itemDelete.Header = "_Delete";
            itemDelete.Click += DeleteOnClick;
            img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,/Images/Cut.png"));
            itemDelete.Icon = img;
            itemEdit.Items.Add(itemDelete);
        }

        private void EditOnOpened(object sender, RoutedEventArgs e)
        {
            itemCut.IsEnabled = itemCopy.IsEnabled = itemDelete.IsEnabled = text.Text.Length > 0;
            itemPaste.IsEnabled = Clipboard.ContainsText();
        }

        private void CutOnClick(object sender, RoutedEventArgs e)
        {
            CopyOnClick(sender, e);
            DeleteOnClick(sender, e);
        }

        private void CopyOnClick(object sender, RoutedEventArgs e)
        {
            if(text.Text != null && text.Text.Length > 0)
                Clipboard.SetText(text.Text);
        }

        private void PasteOnClick(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
                text.Text = Clipboard.GetText();
        }
        private void DeleteOnClick(object sender, RoutedEventArgs e)
        {
            text.Text = null;
        }



    }
}
