using System.Windows;
using System.Windows.Controls;

namespace Petzold.PeruseTheMenu
{
    internal class PeruseTheMenu : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PeruseTheMenu());
        }

        public PeruseTheMenu()
        {
            Title = "Peruse the Menu";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // DockPanel 생성
            DockPanel dock = new DockPanel();
            Content = dock;

            // 탑 메뉴가 될 Menu 생성
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            // 나머지 영역을 채울 TextBlock 생성
            TextBlock text = new TextBlock();
            text.Text = Title;
            text.FontSize = 32;
            text.TextAlignment = TextAlignment.Center;
            dock.Children.Add(text);

            // File 메뉴 생성
            MenuItem itemFile = new MenuItem();
            itemFile.Header = "_File";
            menu.Items.Add(itemFile);

            MenuItem itemNew = new MenuItem();
            itemNew.Header = "_New";
            itemNew.Click += UnimplementedOnClick;
            itemFile.Items.Add(itemNew);

            MenuItem itemOpen = new MenuItem();
            itemOpen.Header = "_Open";
            itemOpen.Click += UnimplementedOnClick;
            itemFile.Items.Add(itemOpen);

            MenuItem itemSave = new MenuItem();
            itemSave.Header = "_Save";
            itemSave.Click += UnimplementedOnClick;
            itemFile.Items.Add(itemSave);

            itemFile.Items.Add(new Separator());

            MenuItem itemExit = new MenuItem();
            itemExit.Header = "E_xit";
            itemExit.Click += ExitOnClick;
            itemFile.Items.Add(itemExit);


            // Window 메뉴 생성
            MenuItem ItemWindow = new MenuItem();
            ItemWindow.Header = "_Window";
            menu.Items.Add(ItemWindow);

            MenuItem itemTaskbar = new MenuItem();
            itemTaskbar.Header = "_Show in Taskbar";
            itemTaskbar.IsCheckable = true;
            //itemTaskbar.IsChecked = ShowInTaskbar;
            //itemTaskbar.Click += TaskbarOnClick;
            itemTaskbar.SetBinding(MenuItem.IsCheckedProperty, "ShowInTaskbar");
            itemTaskbar.DataContext = this;
            ItemWindow.Items.Add(itemTaskbar);

            MenuItem itemSize = new MenuItem();
            itemSize.Header = "Size to _Content";
            itemSize.IsCheckable = true;
            itemSize.IsChecked = SizeToContent == SizeToContent.WidthAndHeight;
            itemSize.Checked += SizeOnCheck;
            itemSize.Unchecked += SizeOnCheck;
            ItemWindow.Items.Add(itemSize);

            MenuItem itemTopmost = new MenuItem();
            itemTopmost.Header = "_Topmost";
            itemTopmost.IsCheckable = true;
            itemTopmost.IsChecked = Topmost;
            itemTopmost.Checked += TopmostOnCheck;
            itemTopmost.Unchecked += TopmostOnCheck;
            ItemWindow.Items.Add(itemTopmost);
        }

        

        private void UnimplementedOnClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            string strItem = item.Header.ToString().Replace("_", "");
            MessageBox.Show("The " + strItem + " option has not yet been implemented", Title);
        }

        private void ExitOnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TaskbarOnClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            ShowInTaskbar = item.IsChecked;
        }

        private void SizeOnCheck(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            ResizeMode = item.IsChecked ? ResizeMode.CanResize : ResizeMode.NoResize;
        }

        private void TopmostOnCheck(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            Topmost = item.IsChecked;
        }
    }
}
