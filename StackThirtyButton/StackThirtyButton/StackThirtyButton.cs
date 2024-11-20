using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace StackThirtyButton
{
    internal class StackThirtyButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new StackThirtyButton());
        }

        public StackThirtyButton()
        {
            Title = "Stack Thirty Buttons";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanResize;
            AddHandler(Button.ClickEvent, new RoutedEventHandler(ButtonOnClick));

            StackPanel stackMain = new StackPanel();
            stackMain.Orientation = Orientation.Horizontal;
            stackMain.Margin = new Thickness(5);
            Content = stackMain;

            for (int i = 0; i < 3; i++)
            {
                StackPanel stackChild = new StackPanel();
                stackMain.Children.Add(stackChild);

                for(int j = 0; j < 10; j++)
                {
                    Button btn = new Button();
                    btn.Content = "Button No. " + (10*i+j+1);
                    btn.Margin = new Thickness(5);
                    stackChild.Children.Add(btn);
                }
            }
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked the button labeled " + (e.Source as Button).Content);
        }
    }
}
