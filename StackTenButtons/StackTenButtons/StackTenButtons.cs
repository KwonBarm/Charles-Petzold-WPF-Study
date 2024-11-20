using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StackTenButtons
{
    internal class StackTenButtons : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new StackTenButtons());
        }

        public StackTenButtons()
        {
            Title = "Stack Ten Buttons";

            StackPanel stack = new StackPanel();
            Content = stack;
            //stack.Background = Brushes.Aquamarine;
            //stack.Orientation = Orientation.Horizontal;
            stack.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Margin = new Thickness(5);

            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                Button btn = new Button();
                btn.Name = ((char)('A' + i)).ToString();
                btn.FontSize += random.Next(10);
                btn.Content = "Button " + btn.Name + " says 'Click me'";
                btn.Margin = new Thickness(5);
                //btn.Click += ButtonOnClick;
                //btn.HorizontalAlignment = HorizontalAlignment.Center;

                stack.Children.Add(btn);
            }

            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            stack.AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonOnClick));

        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;

            MessageBox.Show("Button " + btn.Name + " has been clicked");
        }
    }

}
