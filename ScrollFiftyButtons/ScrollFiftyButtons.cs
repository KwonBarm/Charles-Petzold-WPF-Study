using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ScrollFiftyButtons
{
    class ScrollFiftyButtons : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ScrollFiftyButtons());
        }

        public ScrollFiftyButtons()
        {
            Title = "Scroll Fifty Buttons";
            SizeToContent = SizeToContent.Width;
            AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonOnClick));
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //ScrollViewer scroll = new ScrollViewer();
            //Content = scroll;
            Viewbox view = new Viewbox();
            Content = view;
            

            StackPanel stack = new StackPanel();
            stack.Margin = new Thickness(5);
            stack.Background = Brushes.Aquamarine;
            //scroll.Content = stack;
            view.Child = stack;

            for (int i = 0; i < 50; i++)
            {
                Button btn = new Button();
                btn.Name = "Button" + (i + 1);
                btn.Content = btn.Name + " says 'Click me'";
                stack.Children.Add(btn);
            }
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button? btn = e.Source as Button;

            if(btn != null)
            {
                MessageBox.Show(btn.Name + " has been clicked", "Button Click");
            }
            else
            {
                MessageBox.Show(e.OriginalSource.ToString());
            }
            //else if(sender is Window win)
            //{
            //    MessageBox.Show(win.Name + " has been clicked", "Button Click");
            //}
        }
    }
}
