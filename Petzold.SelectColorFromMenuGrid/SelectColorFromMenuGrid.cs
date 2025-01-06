using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Petzold.SelectColorFromGrid;

namespace Petzold.SelectColorFromMenuGrid
{
    class SelectColorFromMenuGrid : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SelectColorFromMenuGrid());
        }

        public SelectColorFromMenuGrid()
        {
            Title = "Select Color from Menu Grid";

            // DockPanel 생성
            DockPanel dock = new DockPanel();
            Content = dock;

            // 탑 메뉴가 될 Menu 생성
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            // 나머지를 채울 TextBlock 생성
            TextBlock text = new TextBlock();
            text.Text = Title;
            text.FontSize = 32;
            text.TextAlignment = TextAlignment.Center;
            dock.Children.Add(text);

            // 메뉴에 항목을 추가
            MenuItem itemColor = new MenuItem();
            itemColor.Header = "_Color";
            menu.Items.Add(itemColor);

            MenuItem itemForeground = new MenuItem();
            itemForeground.Header = "_Foreground";
            itemColor.Items.Add(itemForeground);

            // 윈도우 전경색과 바인딩되는 ColorGridBox 생성
            ColorGridBox clrBox = new ColorGridBox();
            clrBox.SetBinding(ColorGridBox.SelectedValueProperty, "Foreground");
            clrBox.DataContext = this;
            itemForeground.Items.Add(clrBox);

            MenuItem itemBackground = new MenuItem();
            itemBackground.Header = "_Background";
            itemColor.Items.Add(itemBackground);

            //  윈도우 배경색과 바인딩되는 ColorGridBox 생성
            clrBox = new ColorGridBox();
            clrBox.SetBinding(ColorGridBox.SelectedValueProperty, "Background");
            clrBox.DataContext = this;
            itemBackground.Items.Add(clrBox);

        }
    }
}
