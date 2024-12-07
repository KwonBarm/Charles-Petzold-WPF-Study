using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MeetTheDockers
{
    class MeetTheDockers : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new MeetTheDockers());
        }

        public MeetTheDockers()
        {
            Title = "Meet the Dockers";

            DockPanel dock = new DockPanel();
            Content = dock;
            dock.Background = Brushes.AliceBlue;

            Menu menu = new Menu();
            MenuItem item = new MenuItem();
            item.Header = "Menu";
            menu.Items.Add(item);

            DockPanel.SetDock(menu, Dock.Top);
            dock.Children.Add(menu);

            ToolBar tool = new ToolBar();
            tool.Header = "Toolbar";

            DockPanel.SetDock(tool, Dock.Top);
            dock.Children.Add(tool);

            StatusBar status = new StatusBar();
            StatusBarItem statusBarItem = new StatusBarItem();
            statusBarItem.Content = "Status";
            status.Items.Add(statusBarItem);

            DockPanel.SetDock(status, Dock.Bottom);
            dock.Children.Add(status);

            ListBox lstBox = new ListBox();
            lstBox.Items.Add("List Box Item"); ;

            DockPanel.SetDock(lstBox, Dock.Left);
            dock.Children.Add(lstBox);

            TextBox txtbox = new TextBox();
            txtbox.AcceptsReturn = true;

            dock.Children.Add(txtbox);
            txtbox.Focus();
        }
    }
}
