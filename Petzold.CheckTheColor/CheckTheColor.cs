using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.CheckTheColor
{
    internal class CheckTheColor : Window
    {
        TextBlock text;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CheckTheColor());
        }

        public CheckTheColor()
        {
            Title = "Check the Color";

            // DockPanel 생성
            DockPanel dock = new DockPanel();
            Content = dock;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // 탑 메뉴가 될 Menu 생성
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            // 나머지를 채울 TextBlock 생성
            text = new TextBlock();
            text.Text = Title;
            text.TextAlignment = TextAlignment.Center;
            text.FontSize = 32;
            //text.Background = SystemColors.WindowBrush;
            //text.Foreground = SystemColors.WindowTextBrush;
            //text.Background = new SolidColorBrush(Colors.Red);
            text.Foreground = new SolidColorBrush(Colors.Coral);
            text.Background = new SolidColorBrush(Colors.Gray);

            dock.Children.Add(text);

            // 메뉴 항목 생성
            MenuItem itemColor = new MenuItem();
            itemColor.Header = "_Color";
            menu.Items.Add(itemColor);

            MenuItem itemForeground = new MenuItem();
            itemForeground.Header = "_Foreground";
            itemForeground.SubmenuOpened += ForegroundOnOpened;
            itemColor.Items.Add(itemForeground);

            FillWithColors(itemForeground, ForegroundOnClick);

            MenuItem itemBackground = new MenuItem();
            itemBackground.Header = "_Background";
            itemBackground.SubmenuOpened += BackgroundOnOpened;
            itemColor.Items.Add(itemBackground);

            FillWithColors(itemBackground, BackgroundOnClick);
        }

        

        private void FillWithColors(MenuItem itemParent, RoutedEventHandler handler)
        {
            foreach (PropertyInfo prop in typeof(Colors).GetProperties())
            {
                Color clr = (Color)prop.GetValue(null, null);
                int iCount = 0;

                iCount += clr.R == 0 || clr.R == 255  ? 0 : 1;
                iCount += clr.G == 0 || clr.G == 255 ? 0 : 1;
                iCount += clr.B == 0 || clr.B == 255 ? 0 : 1;

                if(clr.A == 255 && iCount > 1)
                {
                    MenuItem item = new MenuItem();
                    item.Header = "_" +prop.Name;
                    item.Tag = clr;
                    item.Click += handler;
                    itemParent.Items.Add(item);
                }
            }
        }

        private void ForegroundOnOpened(object sender, RoutedEventArgs e)
        {
            MenuItem itemParent = sender as MenuItem;

            foreach (MenuItem item in itemParent.Items)
                item.IsChecked = ((text.Foreground as SolidColorBrush).Color == (Color)item.Tag);
        }

        private void BackgroundOnOpened(object sender, RoutedEventArgs e)
        {
            MenuItem itemParent = sender as MenuItem;

            foreach(MenuItem item in itemParent.Items)
                item.IsChecked = ((text.Background as SolidColorBrush).Color == (Color)item.Tag);
        }

        private void ForegroundOnClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            Color clr = (Color)item.Tag;
            text.Foreground  = new SolidColorBrush(clr);
        }

        private void BackgroundOnClick(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            Color clr = (Color)item.Tag;
            text.Background = new SolidColorBrush(clr);
        }
    }
}
