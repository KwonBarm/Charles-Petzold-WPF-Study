using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SizeToContent = SizeToContent.Width;
            AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonOnClick));

            //ScrollViewer scroll = new ScrollViewer();
            //Content = scroll;

            Viewbox viewbox = new Viewbox();
            Content = viewbox;

            StackPanel stack = new StackPanel();
            stack.Margin = new Thickness(5);
            //scroll.Content = stack;
            viewbox.Child = stack;


            for (int i = 0; i < 50; i++)
            {
                Button btn = new Button();
                btn.Name = "Button" + (i+1);
                btn.Content = btn.Name + " says 'Click me'";
                btn.Margin = new Thickness(5);

                stack.Children.Add(btn);
            }

        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button? btn = e.Source as Button;

            //if(btn != null)
            //{
                MessageBox.Show(btn.Name + " has been Clicked", "Button Click");
            //}
        }
    }
}
