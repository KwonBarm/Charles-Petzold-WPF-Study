using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DockAroundTheBlock
{
    class DockAroundTheBlock : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new DockAroundTheBlock());
        }

        public DockAroundTheBlock()
        {
            Title = "Dock Around The Block";

            DockPanel dock = new DockPanel();
            Content = dock;
            dock.Background = Brushes.Yellow;

            for (int i = 0; i < 17; i++)
            {
                Button btn = new Button();
                btn.Content = "Button No. " + (i + 1);
                dock.Children.Add(btn);
                btn.SetValue(DockPanel.DockProperty, (Dock)(i % 4));
                btn.HorizontalAlignment = HorizontalAlignment.Center;
            }

            dock.LastChildFill = false;
        }
    }
}
