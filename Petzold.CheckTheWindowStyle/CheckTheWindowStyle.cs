using System.Windows;
using System.Windows.Controls;

namespace Petzold.CheckTheWindowStyle
{
    class CheckTheWindowStyle : Window
    {
        MenuItem itemchecked;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CheckTheWindowStyle());
        }

        public CheckTheWindowStyle()
        {
            Title = "Check the Window Style";

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

            // WindowStyle을 변경할 Menuitem 객체를 생성
            MenuItem itemStyle = new MenuItem();
            itemStyle.Header = "_Style";
            menu.Items.Add(itemStyle);

            itemStyle.Items.Add(CreateMenuItem("_No border or caption", WindowStyle.None));
            itemStyle.Items.Add(CreateMenuItem("_Single-border Window", WindowStyle.SingleBorderWindow));
            itemStyle.Items.Add(CreateMenuItem("_Three-D Window", WindowStyle.ThreeDBorderWindow));
            itemStyle.Items.Add(CreateMenuItem("_Tool Window", WindowStyle.ToolWindow));
        }

        private MenuItem CreateMenuItem(string str, WindowStyle style)
        {
            MenuItem item = new MenuItem();
            item.Header = str;
            item.Tag = style;
            item.IsChecked = (style == WindowStyle);
            item.Click += StyleOnClick;

            if (item.IsChecked)
                itemchecked = item;

            return item;
        }

        private void StyleOnClick(object sender, RoutedEventArgs e)
        {
            itemchecked.IsChecked = false;
            itemchecked = e.Source as MenuItem;
            itemchecked.IsChecked = true;

            WindowStyle = (WindowStyle)itemchecked.Tag;
        }
    }
}
